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
    /// <summary>
    /// state command of maze - implement command pattern.
    /// </summary>
    class SolveMazeCommand : ICommand
    {
        /// <summary>
        /// model - mvc server.
        /// </summary>
        private IModel model;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="model">model</param>
        public SolveMazeCommand(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// execute commande and return solution at json format.
        /// </summary>
        /// <param name="args">input from user</param>
        /// <param name="client">client's data</param>
        /// <returns>solution - string at json format</returns>
        public string Execute(string[] args, TcpClient client)
        {
            string name;
            int algo;
            try {
                name = args[0];
                algo = int.Parse(args[1]);
                if (algo != 1 && algo != 0)
                    return "Error at algorithem numeber parameter: 0 - for bfs, 1 - for dfs";
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Error in parameters of solve comand");
                return "Error in parameters of solve comand";
            }
            Solution<MazeLib.Position> solution = model.Solve(name, algo);
            if (solution == null)
                return "name of maze doesn't exist at maze single player pool";
            SolutionRepresent<MazeLib.Direction, MazeLib.Position, int> solRepresent = new MazeSolRepreset(solution);
            solRepresent.ConvertSolution();
            return solRepresent.ToJSON();
        }
    }
}
