using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Service
{
    /// <summary>
    /// Services are managed by the core game object. They provide functions required by all game activities.
    /// </summary>
    public interface IService
    {
        void StartService();

        void EndService();
    }
}
