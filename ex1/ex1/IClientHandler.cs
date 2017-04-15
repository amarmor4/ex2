using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ex1
{
    /// <summary>
    /// interfate of handle client.
    /// </summary>
    interface IClientHandler
    {
        /// <summary>
        /// handle client.
        /// </summary>
        /// <param name="client">client's data</param>
        void HandleClient(TcpClient client);
    }
}
