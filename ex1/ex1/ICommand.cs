using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ex1
{
    /// <summary>
    /// interface of command pattern.
    /// </summary>
    interface ICommand
    {
        /// <summary>
        /// execute commande and return solution at json format.
        /// </summary>
        /// <param name="args">input from user</param>
        /// <param name="client">client's data</param>
        /// <returns>solution - string at json format</returns>
        string Execute(string[] args, TcpClient client = null);
    }
}
