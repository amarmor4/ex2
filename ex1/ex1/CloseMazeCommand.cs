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
        /// view
        /// </summary>
        private IClientHandler view;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="model">model</param>
        public CloseMazeCommand(IModel model, IClientHandler view)
        {
            this.model = model;
            this.view = view;
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
            try {
                name = args[0];
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Error in parameters of close comand");
                return "Error in parameters of close comand";
            }
            if(!model.IsParticipate(client))
                return "Error: client Participating in multiplayer game";
            TcpClient otherParticipate = this.model.GetOtherParticipate(client);
            string temp=this.model.Close(name, client);
            if (temp != null)
                return temp;
            if (otherParticipate != null)
                this.view.SendToClient(this.ToJSON(), otherParticipate);
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
