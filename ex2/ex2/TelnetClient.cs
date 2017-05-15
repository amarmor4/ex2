using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Configuration;

namespace ex2
{
    /// <summary>
    /// TelnetClient
    /// </summary>
    class TelnetClient :ITelnetClient
    {
        /// <summary>
        /// ip and port.
        /// </summary>
        IPEndPoint ep;

        /// <summary>
        /// client tcp socket.
        /// </summary>
        TcpClient client;

        /// <summary>
        /// stream.
        /// </summary>
        NetworkStream stream;

        /// <summary>
        /// reader.
        /// </summary>
        StreamReader reader;

        /// <summary>
        /// writer.
        /// </summary>
        StreamWriter writer;

        /// <summary>
        /// constructor
        /// </summary>
        public TelnetClient()
        {
            int portnum = Properties.Settings.Default.ServerPort;
            string ipAddress = Properties.Settings.Default.ServerIP;
            this.ep = new IPEndPoint(IPAddress.Parse(ipAddress), portnum);
        }

        /// <summary>
        /// connect to server
        /// </summary>
        /// <param name="ip">ip address</param>
        /// <param name="port">port number</param>
        public void Connect()
        {
            this.client = new TcpClient();
            client.Connect(ep);
            stream = client.GetStream();
            this.writer = new StreamWriter(stream);
            this.reader = new StreamReader(stream);
        }

        /// <summary>
        /// send to server command.
        /// </summary>
        /// <param name="command">command</param>
        public void Write(string command)
        {
            this.writer.AutoFlush = true;
            this.writer.WriteLine(command);
        }

        /// <summary>
        /// read from server.
        /// </summary>
        /// <returns>answer</returns>
        public string Read()
        {
            string str="";
            string line;
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                Console.WriteLine(line);
                str=string.Concat(str, line);
            }
            return str;
        }

        /// <summary>
        /// disconnect from server.
        /// </summary>
        public void Disconnect()
        {
            this.client.Close();
        }
    }
}
