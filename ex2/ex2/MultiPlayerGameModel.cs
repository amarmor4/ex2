using MazeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ex2
{
    /// <summary>
    /// multi player game model
    /// </summary>
    class MultiPlayerGameModel : INotifyPropertyChanged
    {
        /// <summary>
        /// maze
        /// </summary>
        Maze maze;

        /// <summary>
        /// list of games
        /// </summary>
        string listOfGames;

        string playMove;

        bool closeGame;

        bool serverFailed;

        /// <summary>
        /// telnet client
        /// </summary>
        ITelnetClient telnetClient;

        ClientMulti clientMulti;

        /// <summary>
        /// property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// constructor.
        /// </summary>
        /// <param name="telnetClient"></param>
        public MultiPlayerGameModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            this.clientMulti = new ClientMulti();
            this.clientMulti.MessageRecvChanged += delegate (string name, string str) {
                NotifyMessageChanged(name, str);
            };
        }

        public void NotifyMessageChanged(string name, string message)
        {
            if(name=="join")
                MazeGame= MazeLib.Maze.FromJSON(message);
            if (name == "move")
                Move = message;
            if (name == "end")
                CloseGame = true;                
        }

        /// <summary>
        /// notify property changed
        /// </summary>
        /// <param name="propName">property name</param>
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// maze game property.
        /// </summary>
        public Maze MazeGame
        {
            get { return this.maze; }
            set
            {
                this.maze = value;
                NotifyPropertyChanged("MazeGame");
            }
        }

        public string ListOfGames
        {
            get { return this.listOfGames; }
            set
            {
                this.listOfGames = value;
                NotifyPropertyChanged("ListOfGames");
            }
        }

        public string Move
        {
            get { return this.playMove; }
            set
            {
                this.playMove = value;
                NotifyPropertyChanged("Move");
            }
        }

        public bool CloseGame
        {
            get { return this.closeGame; }
            set
            {
                this.closeGame = value;
                NotifyPropertyChanged("CloseGame");
            }
        }

        public bool ServerFailed
        {
            get { return this.serverFailed; }
            set
            {
                this.serverFailed = value;
                NotifyPropertyChanged("ServerFailed");
            }
        }

        /// <summary>
        /// list of games to join
        /// </summary>
        public void List()
        {

            if (this.telnetClient.Connect())
            {
                string command = "list";
                this.telnetClient.Write(command);
                Task recv = new Task(() =>
                {
                    string str = this.telnetClient.Read();
                    ListOfGames = str;
                    this.telnetClient.Disconnect();
                });
                recv.Start();
            }
            else
                ServerFailed = true;
        }

        /// <summary>
        /// start multi player game.
        /// </summary>
        /// <param name="command">command</param>
        public void Start(string command)
        {
            if (this.clientMulti.Connect())
                this.clientMulti.Write(command);
            else
                ServerFailed = true;
        }

        /// <summary>
        /// join to game.
        /// </summary>
        /// <param name="command">command</param>
        public void Join(string command)
        {
            if (this.clientMulti.Connect())
                this.clientMulti.Write(command);
            else
                ServerFailed = true;
        }

        /// <summary>
        /// play move in maze
        /// </summary>
        /// <param name="command">command</param>
        public void Play(string command)
        {
            if (!this.clientMulti.Write(command))
                ServerFailed = true;
        }

        /// <summary>
        /// close game.
        /// </summary>
        /// <param name="command"></param>
        public void Close(string command)
        {
            if (!this.clientMulti.Write(command))
                ServerFailed = true;
            this.clientMulti.Disconnect();
        }
    }
}
