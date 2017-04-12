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
    class ListMazeCommand:ICommand
    {
        private IModel model;
        public ListMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            List<String> gameNames = model.List();
            return JsonConvert.SerializeObject(gameNames);
        }
    }
}
