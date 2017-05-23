using MazeLib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex2
{
    /// <summary>
    /// multi player game view model
    /// </summary>
    class MultiPlayerGameViewModel : ViewModel
    {
        /// <summary>
        /// maze game
        /// </summary>
        Maze mazeGame;

        string modePlay;

        /// <summary>
        /// maze name
        /// </summary>
        string name;

        /// <summary>
        /// math path
        /// </summary>
        public string mazePath;

        /// <summary>
        /// currentState
        /// </summary>
        public string initialState;

        /// <summary>
        /// goal state
        /// </summary>
        public string goalState;

        /// <summary>
        /// maze rows
        /// </summary>
        public int rows;

        /// <summary>
        /// maze cols
        /// </summary>
        public int cols;

        /// <summary>
        /// close game
        /// </summary>
        public bool closeGame;

        /// <summary>
        /// server failed
        /// </summary>
        public bool serverFailed;

        /// <summary>
        /// list of games.
        /// </summary>
        public ObservableCollection<string> listOfGames;

        /// <summary>
        /// play move
        /// </summary>
        public string playMove;

        /// <summary>
        /// multi player game model
        /// </summary>
        private MultiPlayerGameModel model;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="model"></param>
        public MultiPlayerGameViewModel(MultiPlayerGameModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChangedModel(e.PropertyName);
            };
            ModePlay = "Multi";
        }

        /// <summary>
        /// PropertyChanged_Model event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged_Model;

        /// <summary>
        /// notify property changed in model
        /// </summary>
        /// <param name="propName"></param>
        public void NotifyPropertyChangedModel(string propName)
        {
            if (propName == "MazeGame")
                MazeGame = model.MazeGame;
            if (propName == "ListOfGames")
            {
                string str = model.ListOfGames;
                ListOfGames= Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<string>>(str);
            }
            if (propName == "Move")
            {
                JObject play = JObject.Parse(model.Move);
                Move = play.GetValue("Direction").ToString();
            }
            if(propName== "CloseGame")
                CloseGame = model.CloseGame;
            if (propName == "ServerFailed")
                ServerFailed = model.ServerFailed;
        }

        /// <summary>
        /// maze game
        /// </summary>
        public Maze MazeGame
        {
            get { return this.mazeGame; }
            set
            {
                this.mazeGame = value;
                this.InitializeMaze();
            }
        }

        /// <summary>
        /// initialize maze when maze game set
        /// </summary>
        public void InitializeMaze()
        {         
            Name = this.mazeGame.Name;
            string strInitial = this.mazeGame.InitialPos.ToString();
            strInitial = strInitial.Replace("(", "");
            strInitial = strInitial.Replace(")", "");
            InitialState = strInitial;
            string strGoal = this.mazeGame.GoalPos.ToString();
            strGoal = strGoal.Replace("(", "");
            strGoal = strGoal.Replace(")", "");
            GoalState = strGoal;
            Rows = this.mazeGame.Rows;
            Cols = this.mazeGame.Cols;
            JObject maze = JObject.Parse(this.mazeGame.ToJSON());
            MazePath = maze.GetValue("Maze").ToString();
        }

        /// <summary>
        /// mode play
        /// </summary>
        public string ModePlay
        {
            get { return this.modePlay; }
            set
            {
                this.modePlay = value;
                NotifyPropertyChanged("ModePlay");
            }
        }

        /// <summary>
        /// name maze property
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                NotifyPropertyChanged("Name");
            }
        }

        /// <summary>
        /// path maze property
        /// </summary>
        public string MazePath
        {
            get { return this.mazePath; }
            set
            {
                this.mazePath = value;
                NotifyPropertyChanged("MazePath");
            }
        }

        /// <summary>
        /// initials state property
        /// </summary>
        public string InitialState
        {
            get { return this.initialState; }
            set
            {
                this.initialState = value;
                NotifyPropertyChanged("InitialState");
            }
        }

        /// <summary>
        /// goal state property
        /// </summary>
        public string GoalState
        {
            get { return this.goalState; }
            set
            {
                this.goalState = value;
                NotifyPropertyChanged("GoalState");
            }
        }

        /// <summary>
        /// rows maze property
        /// </summary>
        public int Rows
        {
            get { return this.rows; }
            set
            {
                this.rows = value;
                NotifyPropertyChanged("Rows");
            }
        }

        /// <summary>
        /// cols maze property
        /// </summary>
        public int Cols
        {
            get { return this.cols; }
            set
            {
                this.cols = value;
                NotifyPropertyChanged("Cols");
            }
        }

        /// <summary>
        /// list of games
        /// </summary>
        public ObservableCollection<string> ListOfGames
        {
            get { return this.listOfGames; }
            set
            {
                this.listOfGames = value;
                NotifyPropertyChanged("ListOfGames");
            }
        }

        /// <summary>
        /// move
        /// </summary>
        public string Move
        {
            get { return this.playMove; }
            set
            {
                this.playMove = value;
                NotifyPropertyChanged("Move");
            }
        }

        /// <summary>
        /// close game
        /// </summary>
        public bool CloseGame
        {
            get { return this.closeGame; }
            set
            {
                this.closeGame = value;
                NotifyPropertyChanged("CloseGame");
            }
        }

        /// <summary>
        /// server failed
        /// </summary>
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
        /// start game - start maze
        /// </summary>
        /// <param name="name">maze name</param>
        /// <param name="rows">rows maze</param>
        /// <param name="cols">cols maze</param>
        public void StartGame(string name, int rows, int cols)
        {
            string command = "start " + name + " " + rows + " " + cols;
            model.Start(command);
        }

        /// <summary>
        /// request to list of game to join
        /// </summary>
        public void GetListToJoin()
        {
            model.List();
        }

        /// <summary>
        /// request to join game
        /// </summary>
        /// <param name="name">maze name</param>
        public void Join(string name)
        {
            string command = "join " + name;
            model.Join(command);
        }

        /// <summary>
        /// play step
        /// </summary>
        /// <param name="move">move</param>
        public void Play(string move)
        {
            string command = "play " + move;
            model.Play(command);
        }

        /// <summary>
        /// close
        /// </summary>
        /// <param name="name">maze name</param>
        public void Close(string name)
        {
            string command = "close " + name;
            model.Close(command);
        }
    }
}
