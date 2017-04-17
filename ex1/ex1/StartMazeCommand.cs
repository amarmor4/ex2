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
    class StartMazeCommand: ICommand
    {
        /// <summary>
        /// model - mvc server.
        /// </summary>
        private IModel model;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="model">model</param>
        public StartMazeCommand(IModel model)
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
            int rows;
            int cols;
            try {
                name = args[0];
                rows = int.Parse(args[1]);
                cols = int.Parse(args[2]);
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Error in parameters of start comand");
                return "Error in parameters of start comand";
            }
            if (this.model.IsParticipate(client))
                return "Error: client Participating in multiplayer game";
            Maze maze = model.Start(name, rows, cols, client);
            if (maze == null)
                return "Error: exist maze with the same name at multiplayer pool or maze generate failed";
            return maze.ToJSON();
        }
    }
}
