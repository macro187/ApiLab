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
                this.uri = uri;
            }


            readonly Uri uri;


            public void Intercept(IInvocation invocation)
            {
                var rpcClient = new RpcClient(uri, (AuthenticationHeaderValue)null);

                var requestTask = rpcClient.SendRequestAsync(
                    invocation.Method.Name,
                    null,
                    invocation.Arguments);

                invocation.ReturnValue = GetReturnValue(requestTask, invocation.Method.ReturnType);
            }


            static object GetReturnValue(Task<RpcResponse> requestTask, Type returnType)
            {
                //
                // Task => Async the request
                //
                if (returnType == typeof(Task))
                {
                    return requestTask;
                }

                //
                // Task<T> => Async the request plus unpack the return value
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
                // Any other type: Sync wait for the request to finish then unpack the result
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
