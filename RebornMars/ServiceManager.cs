using Boyd.Games.RebornMars.Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Service
{
    internal class ServiceManager : IServiceManager
    {
        private Dictionary<Type, IService> Services { get; set; }
        private Dictionary<Type, IActiveService> ActiveServices { get; set; }
        private IGameCore CurrentGame { get; set; }

        internal ServiceManager(IGameCore game)
        {
            CurrentGame = game;
            Services = new Dictionary<Type, IService>();
            ActiveServices = new Dictionary<Type, IActiveService>();

            CurrentGame.TurnCompleted += GameTurnCompleted;
        }

        private void GameTurnCompleted(object sender, EventArgs e)
        {
            UpdateActiveServices();
        }

        public IService GetService(Type serviceType)
        {
            return Services[serviceType];
        }

        public T GetService<T>() where T : IService
        {
            return (T)Services[typeof(T)];
        }

        public void RegisterService(IService service)
        {
            try
            {
                Services.Add(service.GetType(), service);

                IActiveService active = service as IActiveService;

                if(active != null)
                {
                    ActiveServices.Add(active.GetType(), active);
                }
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(string.Format(ErrorMessages.DuplicateServiceMessage, service.GetType().ToString()), ex);
            }

        }

        public void UnregisterService(Type serviceType)
        {
            if (Services.ContainsKey(serviceType))
            {
                Services.Remove(serviceType);
            }

            if (ActiveServices.ContainsKey(serviceType))
            {
                ActiveServices.Remove(serviceType);
            }
        }

        public void UpdateActiveServices()
        {
            foreach(IActiveService activeService in ActiveServices.Values)
            {
                activeService.Update();
            }
        }

        #region IEnumerable<T> Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator1();
        }

        private IEnumerator GetEnumerator1()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<IService> GetEnumerator()
        {
            return this.Services.Values.GetEnumerator();
        }

        #endregion
    }
}
