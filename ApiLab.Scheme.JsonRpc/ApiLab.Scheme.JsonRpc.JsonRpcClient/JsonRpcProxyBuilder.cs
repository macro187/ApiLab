using System;
using System.Net.Http.Headers;
using Castle.DynamicProxy;
using EdjCase.JsonRpc.Client;

namespace ApiLab.Scheme.JsonRpc.JsonRpcClient
{
    public static class JsonRpcProxyBuilder
    {

        /// <summary>
        /// Create a service interface implementation that routes all operations to a JSON-RPC endpoint
        /// </summary>
        ///
        /// <typeparam name="TService">
        /// The service interface
        /// </typeparam>
        ///
        /// <param name="uri">
        /// URL to a JSON-RPC endpoint
        /// </param>
        ///
        /// <returns>
        /// An implementation of <typeparamref name="TService"/> that routes operations to the JSON-RPC endpoint at the
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
                _uri = uri;
            }

            readonly Uri _uri;


            public void Intercept(IInvocation invocation)
            {
                var rpcClient = new RpcClient(_uri, (AuthenticationHeaderValue)null);

                var requestTask = rpcClient.SendRequestAsync(
                    invocation.Method.Name,
                    null,
                    invocation.Arguments);

                requestTask.Wait();

                invocation.ReturnValue = requestTask.Result.Result.ToObject(invocation.Method.ReturnType);
            }

        }

    }
}
