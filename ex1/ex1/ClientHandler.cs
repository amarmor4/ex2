using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace ex1
{
    class ClientHandler: IClientHandler
    {
        Controller c;
        public ClientHandler(Controller con)
        {
            this.c = con;
        }

        public void HandleClient(TcpClient client)
            {
            new Task(() =>
                {
                    using (NetworkStream stream = client.GetStream())
                    using (StreamReader reader = new StreamReader(stream))
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        string commandLine = reader.ReadLine();
                        string result = c.ExecuteCommand(commandLine, client);
                        writer.Write(result);
                    }
                    client.Close();
                }).Start();
            }
        }
}
