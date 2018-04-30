using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using EdjCase.JsonRpc.Client;
using EdjCase.JsonRpc.Core;

namespace ApiLab.Scheme.JsonRpc.Infrastructure.DotNetJsonRpcClient
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
        static public TService Build<TService>(Uri uri)
            where TService : class
        {
            if (!typeof(TService).IsInterface)
                throw new ArgumentException("TService is not an interface", nameof(TService));

            return
                new ProxyGenerator()
                    .CreateInterfaceProxyWithoutTarget<TService>(
                        new Interceptor(uri));
        }


        class Interceptor : IInterceptor
        {

            public Interceptor(Uri uri)
            {
                rpcClient = new RpcClient(uri, (AuthenticationHeaderValue)null);
            }


            readonly RpcClient rpcClient;


            public void Intercept(IInvocation invocation)
            {
                var request = RpcRequest.WithParameterList(
                    invocation.Method.Name,
                    invocation.Arguments,
                    Guid.NewGuid().ToString());

                var requestTask = rpcClient.SendRequestAsync(request);

                invocation.ReturnValue = GetReturnValue(requestTask, invocation.Method.ReturnType);
            }


            /// <summary>
            /// Get service method return value based on the return type
            /// </summary>
            ///
            static object GetReturnValue(Task<RpcResponse> requestTask, Type returnType)
            {
                //
                // Task => The request task itself
                //
                if (returnType == typeof(Task))
                {
                    return requestTask;
                }

                //
                // Task<T> => The request plus unpacking the result as a Task<T>
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
                // Any other type: Wait for the request to finish, unpack the result, and return it
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
