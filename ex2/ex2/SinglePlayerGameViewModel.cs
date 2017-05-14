using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MazeLib;
using SearchAlgorithmsLib;
using MazeGeneratorLib;
using Newtonsoft.Json.Linq;

namespace ex2
{
    class SinglePlayerGameViewModel:ViewModel
    {
        /// <summary>
        /// single player game model
        /// </summary>
        private SinglePlayerGameModel model;

        /// <summary>
        /// maze game
        /// </summary>
        Maze mazeGame;

        /// <summary>
        /// maze solution
        /// </summary>
        Solution<Position> mazeSolve;

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
        public Position initialState;

        /// <summary>
        /// goal state
        /// </summary>
        public Position goalState;

        /// <summary>
        /// maze rows
        /// </summary>
        public int rows;

        /// <summary>
        /// maze cols
        /// </summary>
        public int cols;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="model">single player game model</param>
        public SinglePlayerGameViewModel(SinglePlayerGameModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate(Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChangedModel(e.PropertyName);
            };
            
        }

        public event PropertyChangedEventHandler PropertyChanged_Model;

        /// <summary>
        /// notify property changed in model
        /// </summary>
        /// <param name="propName"></param>
        public void NotifyPropertyChangedModel(string propName)
        {
            if (propName == "MazeGame")
                MazeGame = model.MazeGame;
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
        /// 
        /// </summary>
        public Solution<Position> MazeSolve
        {
            get { return this.mazeSolve; }
            set
            {
                this.mazeSolve = value;
                JObject solve = JObject.Parse(this.mazeSolve.ToString());
                string str = solve.GetValue("Solve").ToString();
            }

        }

        /// <summary>
        /// initialize maze when maze game set
        /// </summary>
        public void InitializeMaze()
        {
            Name = this.mazeGame.Name;
            InitialState = this.mazeGame.InitialPos;
            GoalState = this.mazeGame.GoalPos;
            Rows = this.mazeGame.Rows;
            Cols = this.mazeGame.Cols;
            JObject maze = JObject.Parse(this.mazeGame.ToJSON());
            MazePath = maze.GetValue("Maze").ToString();
        }

        /// <summary>
        /// name maze property
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set {
                this.name=value;
                NotifyPropertyChanged("Name");
            }
        }

        /// <summary>
        /// path maze property
        /// </summary>
        public string MazePath
        {
            get { return this.mazePath; }
            set {
                this.mazePath = value;
                NotifyPropertyChanged("MazePath");
            }
        }

        /// <summary>
        /// initials state property
        /// </summary>
        public Position InitialState
        {
            get { return this.initialState; }
            set {
                this.initialState = value;
                NotifyPropertyChanged("InitialState");
            }
        }

        /// <summary>
        /// goal state property
        /// </summary>
        public Position GoalState
        {
            get { return this.goalState; }
            set {
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
        /// start game - generate maze
        /// </summary>
        /// <param name="name">maze name</param>
        /// <param name="rows">rows maze</param>
        /// <param name="cols">cols maze</param>
        public void StartGame(string name, int rows, int cols)
        { 
            string command = "generate " + name + " " + rows + " " + cols;
            model.Start(command);
        }

        /// <summary>
        /// solve game
        /// </summary>
        /// <param name="name">maze name</param>
        public void SolveGame(string name)
        {
            int algo=Properties.Settings.Default.SearchAlgorithm;
            string command = "solve " + name + " " +algo;
            model.Solve(command);
        }
    }
}
