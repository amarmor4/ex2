using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace ex1
{
    /// <summary>
    /// two players game
    /// </summary>
    class Game
    {

        Player playerOne;

        Player playerTwo;

        /// <summary>
        /// game name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// maze.
        /// </summary>
        public Maze Maze { get; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name">game name</param>
        /// <param name="maze">maze</param>
        /// <param name="player2">player one - socket</param>
        public Game(string name, Maze mazeGame, TcpClient client)
        {
            this.Name = name;
            this.Maze = mazeGame;
            this.playerOne = new Player(client);
            this.playerTwo = null;
        }

        public bool WaitToAnotherPlayer()
        {
            while (this.playerTwo==null) { }
            return true; 
        }

        public bool AddSecondPlayer(TcpClient client)
        {
            if (this.playerTwo != null)
            {
                return false;
            }
            this.playerTwo = new Player(client);
            return true;
        }

        public TcpClient GetOtherPlayer(TcpClient currentPlayer)
        {
            if (currentPlayer.Equals(this.playerOne.Client))
            {
                if(this.playerTwo.IsConnected)
                    return this.playerTwo.Client;
            }
            else
            {
                 if(this.playerOne.IsConnected)   
                    return this.playerOne.Client;
            }
            return null;
        }

        public void DisConnectPlayer(TcpClient currentPlayer)
        {
            if (currentPlayer.Equals(this.playerOne.Client))
                this.playerOne.IsConnected = false;
            else if (currentPlayer.Equals(this.playerTwo.Client))
                this.playerTwo.IsConnected = false;
        }

        public string MoveToJSON(Direction move)
        {
            string strMove = move.ToString();
            JObject moveObj = new JObject();
            moveObj["Name"] = this.Name;
            moveObj["Direction"] = char.ToLower(strMove[0]) + strMove.Substring(1);
            return moveObj.ToString();
        }
    }
}
