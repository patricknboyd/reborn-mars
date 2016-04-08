using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Games.RebornMars.UI.Shell
{
    class Program
    {
        static void Main(string[] args)
        {
            GameWrapper app = new GameWrapper();

            app.Run();

            Console.Clear();
            //Console.WriteLine("Thanks for playing.");
            //Console.WriteLine("Press <Enter> to quit...");

            //Console.ReadLine();
        }
    }
}
