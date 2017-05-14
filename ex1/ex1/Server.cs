using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
//test2
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
        public Server(IClientHandler ch)
        {
            int portnum;
            string portFromAppConfig = ConfigurationManager.AppSettings["ServerPort"].ToString();
            bool getPort = Int32.TryParse(portFromAppConfig, out portnum);
            if (!getPort)
                throw new System.InvalidOperationException("port in app.config not an integer");
            this.port = portnum;
            this.ch = ch;
        }

        /// <summary>
        /// upload server.
        /// </summary>
        public void Start()
        {
            string ipAddress = ConfigurationManager.AppSettings["ServerIP"].ToString();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ipAddress), this.port);
            listener = new TcpListener(ep);

            listener.Start();
            //Console.WriteLine("Waiting for connections...");

            Task task = new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        //Console.WriteLine("Got new connection");
                        ch.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                //Console.WriteLine("Server stopped");
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
