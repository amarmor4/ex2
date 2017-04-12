using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ex1
{
    class Controller
    {
        private Dictionary<string, ICommand> commands;
        private IModel model;
        private Server view;
        public Controller()
        {
            this.commands = new Dictionary<string, ICommand>();
        }
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

        public void setView(Server v)
        {
            this.view = v;
        }

        public void setModel(IModel m)
        {
            this.model = m;
            addCommands();
        }

        public void addCommands()
        {
            this.commands.Add("generate", new GenerateMazeCommand(model));
            this.commands.Add("solve", new SolveMazeCommand(model));
            this.commands.Add("start", new StartMazeCommand(model));
            this.commands.Add("list", new ListMazeCommand(model));
            this.commands.Add("join", new JoinMazeCommand(model));
            this.commands.Add("play", new PlayMazeCommand(model));
            this.commands.Add("close", new CloseMazeCommand(model));
        }
    }
}
