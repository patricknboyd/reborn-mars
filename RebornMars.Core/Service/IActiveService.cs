using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Service
{
    /// <summary>
    /// An active service has functionality that will be executed by the game every turn.
    /// </summary>
    public interface IActiveService : IService
    {
        void Update();
    }
}
