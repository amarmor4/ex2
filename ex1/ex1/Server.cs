using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ex1
{
    /// <summary>
    /// server side.
    /// </summary>
    class Server
    {
        /// <summary>
        /// port number
        /// </summary>
        private int port;

        /// <summary>
        /// listener to clients
        /// </summary>
        private TcpListener listener;

        /// <summary>
        /// client handler tasks.
        /// </summary>
        private IClientHandler ch;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="port">port number</param>
        /// <param name="ch">client handler</param>
        public Server(int port, IClientHandler ch)
        {
            this.port = port;
            this.ch = ch;
        }

        /// <summary>
        /// upload server.
        /// </summary>
        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);

            listener.Start();
            Console.WriteLine("Waiting for connections...");

            Task task = new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        ch.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
        }
 
        /// <summary>
        /// stop listening to clients.
        /// </summary>
        public void Stop()
        {
            listener.Stop();
        }
    }
}
