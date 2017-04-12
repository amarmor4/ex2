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
    class PlayMazeCommand: ICommand
    {
        private IModel model;
        public PlayMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            MazeLib.Direction move =(MazeLib.Direction) Enum.Parse(typeof(MazeLib.Direction), args[0]);
            MazeLib.Direction move1 = model.Play(move);
            return JsonConvert.SerializeObject(move1);
        }
    }
}
