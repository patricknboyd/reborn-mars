using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Service
{
    /// <summary>
    /// Stores any services registerd to the game, and allows retrieval of services.
    /// </summary>
    public interface IServiceManager : IEnumerable<IService>
    {
        /// <summary>
        /// Registers a new service.
        /// </summary>
        /// <param name="service">The service instance to register.</param>
        void RegisterService(IService service);
        /// <summary>
        /// Removes a registered service.
        /// </summary>
        /// <param name="serviceType">The type of the service to unregister.</param>
        void UnregisterService(Type serviceType);

        /// <summary>
        /// Gets a service that matches the provided type.
        /// </summary>
        /// <param name="serviceType">The service type to retrieve.</param>
        /// <returns>The service instance, if one of that type exists, otherwise null.</returns>
        IService GetService(Type serviceType);

        /// <summary>
        /// Gets a service that matches the provided type.
        /// </summary>
        /// <typeparam name="T">The service type to retrieve.</typeparam>
        /// <returns>The service instance, if one of that type exists, otherwise null.</returns>
        T GetService<T>()
            where T : IService;

        /// <summary>
        /// Triggers the update for all registered active services.
        /// </summary>
        void UpdateActiveServices();
    }
}
