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
    class CloseMazeCommand:ICommand
    {
        private IModel model;
        public CloseMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            bool close = this.model.Close(name);
            return this.ToJSON();
        }

        private string ToJSON()
        {
            JObject mazeObj = new JObject();
            return mazeObj.ToString();
        }
    }
}
