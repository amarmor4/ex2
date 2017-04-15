using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;
using MazeGeneratorLib;

namespace ex1
{
    /// <summary>
    /// implelemt model of maze.
    /// </summary>
    class MazeModel : IModel
    {
        /// <summary>
        /// pool of solution.
        /// </summary>
        Dictionary<string, Solution<Position>> poolSolutions;

        /// <summary>
        /// pool of mazes.
        /// </summary>
        Dictionary<string, Maze> poolMazes;

        /// <summary>
        /// list of games that can join.
        /// </summary>
        List<string> gamesToJoin;

        /// <summary>
        /// controller.
        /// </summary>
        Controller c;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="con">controller</param>
        public MazeModel(Controller con)
        {
            this.poolSolutions = new Dictionary<string, Solution<Position>>();
            this.poolMazes = new Dictionary<string, Maze>();
            this.gamesToJoin = new List<string>();
            this.c = con;
        }

        /// <summary>
        /// Generate a maze for single player.
        /// </summary>
        /// <param name="name">maze name</param>
        /// <param name="rows">number of rows at maze.</param>
        /// <param name="cols">number of cols at maze.</param>
        /// <returns>maze</returns>
        public Maze Generate(string name, int rows, int cols)
        {
            IMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(rows, cols);
            maze.Name = name;
            if (!this.poolMazes.ContainsKey(name))
            {
                this.poolMazes.Add(name, maze);
            }
            else
            {
                this.poolMazes[name] = maze;
                if (this.poolSolutions.ContainsKey(name))
                    this.poolSolutions.Remove(name);
            }
            return maze;
        }

        /// <summary>
        /// solve the maze problem.
        /// </summary>
        /// <param name="name">maze name</param>
        /// <param name="algo">0-bfs, 1-dfs</param>
        /// <returns>get solution of maze problem</returns>
        public Solution<Position> Solve(string name, int algo)
        {
            if (!this.poolSolutions.ContainsKey(name))
            {
                ISearcher<Position> searchAlgo;
                Solution<Position> solution;
                Maze maze = this.poolMazes[name];
                Adapter<Position> adapter = new MazeToSearchableAdapter<Position>(maze);
                ISearchable<Position> searchableMaze = new Searchable<Position, Direction>(adapter);
                switch (algo)
                {
                    case 0:
                        searchAlgo = new Bfs<Position>();
                        break;
                    case 1:
                        searchAlgo=new Dfs<Position>();
                        break;
                    default:
                        searchAlgo = null;
                        break;
                }
                if (searchAlgo != null)
                {
                    solution = searchAlgo.Search(searchableMaze);
                    this.poolSolutions.Add(name, solution);
                }
            }
            return this.poolSolutions[name];
        }

        /// <summary>
        /// Generate a maze for two players.
        /// </summary>
        /// <param name="name">maze name</param>
        /// <param name="rows">number of rows at maze.</param>
        /// <param name="cols">number of cols at maze.</param>
        /// <returns>maze</returns>
        public Maze Start(string name, int rows, int cols)
        {
            Maze maze=Generate(name, rows, cols);
            this.gamesToJoin.Add(name);
            //toDo wait to join and stuff.
            return maze;
        }

        /// <summary>
        /// list of games that can join - two players.
        /// </summary>
        /// <returns>list of games that can join</returns>
        public List<string> List()
        {
            return this.gamesToJoin;
        }

        /// <summary>
        /// join to game of two players.
        /// </summary>
        /// <param name="name">maze name</param>
        /// <returns>maze</returns>
        public Maze Join(string name)
        {
            if (!this.gamesToJoin.Contains(name))
                return null;
            this.gamesToJoin.Remove(name);
            return this.poolMazes[name];
        }

        /// <summary>
        /// play one move, in two playres game.
        /// </summary>
        /// <param name="move">direction of player at maze.</param>
        /// <returns>the move</returns>
        public Direction Play(Direction move)
        {
            return move;
        }

        /// <summary>
        /// close the maze between to playres.
        /// </summary>
        /// <param name="name">maze name</param>
        /// <returns>true - Succeeded in close</returns>
        public bool Close(string name)
        {
            try {
                if (this.gamesToJoin.Contains(name))
                    this.gamesToJoin.Remove(name);
            }
            catch (Exception) { return false; }
            return true;
        }
    }
}
