using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex2
{
    /// <summary>
    ///  interface TelnetClient
    /// </summary>
    interface ITelnetClient
    {
        /// <summary>
        /// connect to server
        /// </summary>
        bool Connect();

        /// <summary>
        /// send to server command.
        /// </summary>
        /// <param name="command">command</param>
        void Write(string command);

        /// <summary>
        /// read from server.
        /// </summary>
        /// <returns>answer</returns>
        string Read(); // blocking call

        /// <summary>
        /// disconnect from server.
        /// </summary>
        void Disconnect();
    }
}
