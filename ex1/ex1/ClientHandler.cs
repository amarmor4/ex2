﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace ex1
{
    /// <summary>
    /// implelment handle client.
    /// </summary>
    class ClientHandler : IClientHandler
    {
        /// <summary>
        /// controller
        /// </summary>
        Controller c;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="con">controller</param>
        public ClientHandler(Controller con)
        {
            this.c = con;
        }

        /// <summary>
        /// handle client.
        /// </summary>
        /// <param name="client">client's data</param>
        public void HandleClient(TcpClient client)
        {
            
            new Task(() =>
                {
                    using (NetworkStream stream = client.GetStream())
                    using (StreamReader reader = new StreamReader(stream))
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        while (true)
                        {
                        try
                            {
                                string commandLine = reader.ReadLine();
                                string result = c.ExecuteCommand(commandLine.ToString(), client);
                                string[] arr = commandLine.Split(' ');
                                string commandKey = arr[0];
                                string str = string.Concat(result, "\nEnd of command" + "\n" + commandKey);
                                writer.AutoFlush = true;
                                writer.WriteLine(str);                                 
                            }
                            catch (Exception) {
                                break;
                            }
                        }
                    }
                    //client.Close();
                }).Start();
        }

        /// <summary>
        /// send string to client
        /// </summary>
        /// <param name="str">string to send</param>
        /// <param name="client">client</param>
        public void SendToClient(string str, TcpClient client, string commandKey)
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            string result= string.Concat(str, "\nEnd of command" + "\n" + commandKey);
            writer.AutoFlush = true;
            writer.WriteLine(result);
        }
    }
}
