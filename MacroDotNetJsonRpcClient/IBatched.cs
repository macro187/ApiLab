using System;
using System.Threading.Tasks;

namespace MacroDotNetJsonRpcClient
{

    /// <summary>
    /// An interface to a service that performs operations in batches
    /// </summary>
    ///
    public interface IBatched<TService>
        where TService : class
    {

        /// <summary>
        /// Invoke a batch of two operations
        /// </summary>
        ///
        /// <param name="operation1">
        /// A lambda that invokes an individual operation on the service
        /// </param>
        ///
        /// <param name="operation2">
        /// A lambda that invokes another individual operation on the service
        /// </param>
        ///
        /// <returns>
        /// A task yielding a tuple containing the return values of the operations in the order specified
        /// </returns>
        ///
        /// <remarks>
        /// The operations do not run in any particular order and may even run in parallel.
        /// </remarks>
        ///
        Task<(T1,T2)> InvokeBatch<T1,T2>(
            Func<TService,T1> operation1,
            Func<TService,T2> operation2);


        // TODO Additional overloads that take additional numbers of operations

    }
}
