using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using EdjCase.JsonRpc.Client;
using EdjCase.JsonRpc.Core;

namespace MacroDotNetJsonRpcClient
{
    public static class JsonRpcClient
    {

        /// <summary>
        /// Create an interface implementation whose operations route to a JSON-RPC endpoint
        /// </summary>
        ///
        /// <typeparam name="TService">
        /// The interface
        /// </typeparam>
        ///
        /// <param name="uri">
        /// URL to a JSON-RPC endpoint
        /// </param>
        ///
        /// <returns>
        /// An implementation of <typeparamref name="TService"/> whose operations route to the JSON-RPC endpoint at the
        /// specified <paramref name="uri"/>
        /// </returns>
        ///
        /// <remarks>
        /// Operations returning <see cref="Task"/>s return the JSON-RPC request tasks themselves.
        ///
        /// Operations returning <see cref="Task`1"/>s return tasks that complete the JSON-RPC requests and unpack
        /// the return values from the responses.
        ///
        /// Operations returning other types block until the JSON-RPC requests complete, unpack the return values
        /// from the responses, and return them.
        /// </remarks>
        ///
        static public TService Build<TService>(Uri uri)
            where TService : class
        {
            if (!typeof(TService).IsInterface)
                throw new ArgumentException("TService is not an interface", nameof(TService));

            return
                (TService)new ProxyGenerator()
                    .CreateInterfaceProxyWithoutTarget(
                        typeof(TService),
                        new []{ typeof(IBatched<TService>) },
                        new Interceptor<TService>(uri));
        }


        class Interceptor<TService> : IInterceptor
            where TService : class
        {

            public Interceptor(Uri uri)
            {
                rpcClient = new RpcClient(uri, (AuthenticationHeaderValue)null);
                invokeBatchGenericMethod =
                    typeof(IBatched<TService>)
                        .GetMethod(nameof(IBatched<TService>.InvokeBatch))
                        .GetGenericMethodDefinition();
                batchRequests = new List<RpcRequest>();
                inBatch = false;
            }


            readonly RpcClient rpcClient;
            readonly MethodInfo invokeBatchGenericMethod;
            readonly IList<RpcRequest> batchRequests;
            bool inBatch;


            public void Intercept(IInvocation invocation)
            {
                var isInvokeBatch =
                    invocation.Method.IsGenericMethod &&
                    invocation.Method.GetGenericMethodDefinition() == invokeBatchGenericMethod;

                if (isInvokeBatch)
                {
                    InvokeBatch(invocation);
                }
                else
                {
                    InvokeServiceMethod(invocation);
                }
            }


            void InvokeServiceMethod(IInvocation invocation)
            {
                var request = BuildRpcRequest(invocation);

                if (inBatch)
                {
                    batchRequests.Add(request);
                }
                else
                {
                    var requestTask = rpcClient.SendRequestAsync(request);
                    invocation.ReturnValue = GetReturnValue(requestTask, invocation.Method.ReturnType);
                }
            }


            void InvokeBatch(IInvocation invocation)
            {
                var invokeBatchGeneric =
                    ((Func<TService,Func<TService,object>,Func<TService,object>,Task<(object,object)>>)InvokeBatch)
                        .Method
                        .GetGenericMethodDefinition()
                        .MakeGenericMethod(
                            invocation.GenericArguments[0],
                            invocation.GenericArguments[1]);

                invocation.ReturnValue = invokeBatchGeneric.Invoke(
                    this,
                    new[]{
                        invocation.Proxy,
                        invocation.Arguments[0],
                        invocation.Arguments[1]
                    });
            }


            Task<(T1,T2)> InvokeBatch<T1,T2>(
                TService proxy,
                Func<TService,T1> operation1,
                Func<TService,T2> operation2)
            {
                BeginBatch();
                operation1(proxy);
                operation2(proxy);
                var requests = EndBatch();

                return
                    rpcClient.SendBulkRequestAsync(requests)
                    .ContinueWith(responses => (
                        responses.Result[0].GetResult<T1>(false),
                        responses.Result[1].GetResult<T2>(false)
                    ));
            }


            void BeginBatch()
            {
                if (inBatch) throw new InvalidOperationException();
                inBatch = true;
            }


            IList<RpcRequest> EndBatch()
            {
                if (!inBatch) throw new InvalidOperationException();
                var requests = batchRequests.ToList();
                batchRequests.Clear();
                inBatch = false;
                return requests;
            }


            static RpcRequest BuildRpcRequest(IInvocation invocation)
            {
                return
                    RpcRequest.WithParameterList(
                        invocation.Method.Name,
                        invocation.Arguments,
                        Guid.NewGuid().ToString());
            }


            /// <summary>
            /// Get service method return value based on its return type
            /// </summary>
            ///
            static object GetReturnValue(Task<RpcResponse> requestTask, Type returnType)
            {
                //
                // Task return type: The request task itself
                //
                if (returnType == typeof(Task))
                {
                    return requestTask;
                }

                //
                // Task<T> return type: Task that completes the request then unpacks the result
                //
                else if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
                {
                    Type t = returnType.GetGenericArguments().Single();

                    var unpackAsync =
                        ((Func<Task<RpcResponse>,Task<object>>)UnpackAsync<object>)
                            .Method
                            .GetGenericMethodDefinition()
                            .MakeGenericMethod(t);

                    return unpackAsync.Invoke(null, new[]{ requestTask });
                }

                //
                // Any other return type: Wait for the request to finish, unpack the result, and return it
                //
                else
                {
                    requestTask.Wait();

                    var unpack =
                        ((Func<Task<RpcResponse>,object>)Unpack<object>)
                            .Method
                            .GetGenericMethodDefinition()
                            .MakeGenericMethod(returnType);

                    return unpack.Invoke(null, new[]{ requestTask });
                }
            }


            static Task<T> UnpackAsync<T>(Task<RpcResponse> requestTask)
            {
                return requestTask.ContinueWith(Unpack<T>);
            }


            static T Unpack<T>(Task<RpcResponse> requestTask)
            {
                return requestTask.Result.GetResult<T>(false);
            }

        }

    }
}
