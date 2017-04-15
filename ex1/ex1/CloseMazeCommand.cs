using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Sockets;

namespace ex1
{
    /// <summary>
    /// state command of maze - implement command pattern.
    /// </summary>
    class CloseMazeCommand :ICommand
    {
        /// <summary>
        /// model - mvc server.
        /// </summary>
        private IModel model;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="model">model</param>
        public CloseMazeCommand(IModel model)
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
            string name = args[0];
            bool close = this.model.Close(name);
            return this.ToJSON();
        }

        /// <summary>
        /// convert object to json
        /// </summary>
        /// <returns>object at json format</returns>
        private string ToJSON()
        {
            JObject mazeObj = new JObject();
            return mazeObj.ToString();
        }
    }
}
