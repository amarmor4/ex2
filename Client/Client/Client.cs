using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Configuration;

//noteee
namespace Client
{
    /// <summary>
    /// client side
    /// </summary>
    class Client
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
        /// is connect.
        /// </summary>
        bool isConnected;

        /// <summary>
        /// constructor.
        /// </summary>
        public Client()
        {
            int portnum;
            string portFromAppConfig = ConfigurationManager.AppSettings["ServerPort"].ToString();
            bool getPort = Int32.TryParse(portFromAppConfig, out portnum);
            if (!getPort)
                throw new System.InvalidOperationException("port in app.config not an integer");
            string ipAddress = ConfigurationManager.AppSettings["ServerIP"].ToString();
            this.ep = new IPEndPoint(IPAddress.Parse(ipAddress), portnum);
            Console.WriteLine("You are connected");
            isConnected = false; ;
        }

        /// <summary>
        /// upload client side.
        /// </summary>
        public void Start()
        {
            Task send = new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        
                        string command = Console.ReadLine();
                        if (!isConnected)
                        {
                            isConnected = true;
                            this.client = new TcpClient();
                            client.Connect(ep);
                            stream = client.GetStream();
                            this.writer = new StreamWriter(stream);
                            this.reader = new StreamReader(stream);
                            this.writer.AutoFlush = true;
                            this.writer.WriteLine(command);
                            Task recv = new Task(() =>
                            {
                                List<string> result= new List<string>(); 
                                while (!reader.EndOfStream)
                                {
                                    string line = reader.ReadLine();
                                    Console.WriteLine(line);
                                    result.Add(line);
                                    /*
                                    // if need to close programe when get empty json - need those lines 
                                    string empty = "{}";
                                    if (string.Compare(line, empty) == 0)
                                        break;
                                        */
                                }

                            });
                            recv.Start(); 
                        }
                        else
                        {
                            this.writer.AutoFlush = true;
                            this.writer.WriteLine(command);
                        }
                    }
                    catch (Exception) { break; }
                }
            });
            send.Start();

            new System.Threading.AutoResetEvent(false).WaitOne();
            //client.Close();
        }
    }
    
}
