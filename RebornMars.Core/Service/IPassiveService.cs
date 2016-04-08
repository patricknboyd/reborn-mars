using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.Service
{
    /// <summary>
    /// A passive service does not need to be updated every turn.
    /// </summary>
    public interface IPassiveService : IService
    {
    }
}
