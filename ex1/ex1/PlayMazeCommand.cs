using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using Newtonsoft.Json;
using System.Net.Sockets;

namespace ex1
{
    /// <summary>
    /// state command of maze - implement command pattern.
    /// </summary>
    class PlayMazeCommand : ICommand
    {
        /// <summary>
        /// model - mvc server.
        /// </summary>
        private IModel model;
        private IClientHandler view;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="model">model</param>
        public PlayMazeCommand(IModel model, IClientHandler view)
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
            MazeLib.Direction move;
            string direction;
            try
            {
                direction = args[0];
                move = (MazeLib.Direction)Enum.Parse(typeof(MazeLib.Direction),char.ToUpper(direction[0])+ direction.Substring(1));
                
            }            
            catch (Exception)
            {
                Console.Error.WriteLine("Error in parameters of play comand");
                return "Error in parameters of play comand";
            }
            if (!model.IsParticipate(client))
                return "Error: client don't Participating in multiplayer game";
            string strMove = this.model.Play(move, client);     
            TcpClient otherClient=this.model.GetOtherParticipate(client);
            if (otherClient == null)
                return "other client close connection";
            this.view.SendToClient(strMove, otherClient);
            return "move send";
        }
    }
}
