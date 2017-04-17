using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ex1
{
    /// <summary>
    /// controller - mvc at server side.
    /// </summary>
    class Controller
    {
        /// <summary>
        /// dictionary of commands.
        /// </summary>
        private Dictionary<string, ICommand> commands;

        /// <summary>
        /// model
        /// </summary>
        private IModel model;

        /// <summary>
        /// view
        /// </summary>
        private IClientHandler view;

        /// <summary>
        /// constructor
        /// </summary>
        public Controller()
        {
            this.commands = new Dictionary<string, ICommand>();
        }

        /// <summary>
        /// execute command.
        /// </summary>
        /// <param name="commandLine">input</param>
        /// <param name="client">client</param>
        /// <returns>json format</returns>
        public string ExecuteCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
                return "Command not found";
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            return command.Execute(args, client);
        }

        /// <summary>
        /// set view.
        /// </summary>
        /// <param name="v">ClientHandler</param>
        public void setView(IClientHandler c)
        {
            this.view = c;
        }

        /// <summary>
        /// set model
        /// </summary>
        /// <param name="m">model</param>
        public void setModel(IModel m)
        {
            this.model = m;
            addCommands();
        }

        /// <summary>
        /// add commands of maze to dictionary.
        /// </summary>
        public void addCommands()
        {
            this.commands.Add("generate", new GenerateMazeCommand(model));
            this.commands.Add("solve", new SolveMazeCommand(model));
            this.commands.Add("start", new StartMazeCommand(model));
            this.commands.Add("list", new ListMazeCommand(model));
            this.commands.Add("join", new JoinMazeCommand(model));
            this.commands.Add("play", new PlayMazeCommand(model, view));
            this.commands.Add("close", new CloseMazeCommand(model, view));
        }
    }
}
