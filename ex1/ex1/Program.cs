using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;
using MazeGeneratorLib;

namespace ex1
{
    /// <summary>
    /// main of server side
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Controller c = new Controller();
            IClientHandler ch = new ClientHandler(c);
            Server server = new Server(ch);
            c.setView(ch);
            IModel m = new MazeModel(c);
            c.setModel(m);
            server.Start();
            Console.ReadKey();
        }
    }
}
