using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using System.Net.Sockets;

namespace ex1
{
    class SolveMazeCommand: ICommand
    {
        private IModel model;
        public SolveMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            int algo = int.Parse(args[1]);
            Solution<Position> solution = model.Solve(name, algo);
            return solution.ToJSON();
        }
    }
}
