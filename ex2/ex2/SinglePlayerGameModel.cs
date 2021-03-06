﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Configuration;
using System.ComponentModel;
using MazeLib;
using SearchAlgorithmsLib;
using MazeGeneratorLib;
using Newtonsoft.Json;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace ex2
{
    /// <summary>
    /// single player game model
    /// </summary>
    class SinglePlayerGameModel : INotifyPropertyChanged
    {
        /// <summary>
        /// maze
        /// </summary>
        Maze maze;

        /// <summary>
        /// solution
        /// </summary>
        string solve;

        bool serverFailed;

        /// <summary>
        /// telnet client 
        /// </summary>
        ITelnetClient telnetClient;

        /// <summary>
        /// property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

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

        /// <summary>
        /// maze solution property.
        /// </summary>
        public string MazeSolve
        {
            get { return this.solve; }
            set
            {
                this.solve = value;
                NotifyPropertyChanged("MazeSolve");
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
        /// notify property changed
        /// </summary>
        /// <param name="propName">property name</param>
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// constructor.
        /// </summary>
        public SinglePlayerGameModel(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
        }

        /// <summary>
        /// request to generate maze.
        /// </summary>
        /// <param name="command">command</param>
        public void Start(string command)
        {
            if (this.telnetClient.Connect())
            {
                this.telnetClient.Write(command);
                Task recv = new Task(() =>
                {
                    string str = this.telnetClient.Read();
                    this.telnetClient.Disconnect();
                    MazeGame = MazeLib.Maze.FromJSON(str);
                });
                recv.Start();
            }
            else
            {
                ServerFailed = true;
            }         
        }

        /// <summary>
        /// request to solve maze.
        /// </summary>
        /// <param name="command">command</param>
        public void Solve(string command)
        {
            if (this.telnetClient.Connect())
            {
                this.telnetClient.Write(command);
                Task recv = new Task(() =>
                {
                    string str = this.telnetClient.Read();
                    this.telnetClient.Disconnect();
                    JObject sol = JObject.Parse(str);
                    MazeSolve = sol.GetValue("Solution").ToString();
                });
                recv.Start();
            }
            else
            {
                ServerFailed = true;
            }
        }
    }
}