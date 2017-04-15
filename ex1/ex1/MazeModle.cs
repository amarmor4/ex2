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
    class MazeModel : IModel
    {
        Dictionary<string, Solution<Position>> poolSolutions;
        Dictionary<string, Maze> poolMazes;
        List<string> gamesToJoin;
        Controller c;

        public MazeModel(Controller con)
        {
            this.poolSolutions = new Dictionary<string, Solution<Position>>();
            this.poolMazes = new Dictionary<string, Maze>();
            this.gamesToJoin = new List<string>();
            this.c = con;
        }
        
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
                    solution = searchAlgo.search(searchableMaze);
                    this.poolSolutions.Add(name, solution);
                }
            }
            return this.poolSolutions[name];
        }

        public Maze Start(string name, int rows, int cols)
        {
            Maze maze=Generate(name, rows, cols);
            this.gamesToJoin.Add(name);
            //toDo wait to join and stuff.
            return maze;
        }

        public List<string> List()
        {
            return this.gamesToJoin;
        }

        public Maze Join(string name)
        {
            if (!this.gamesToJoin.Contains(name))
                return null;
            this.gamesToJoin.Remove(name);
            return this.poolMazes[name];
        }

        public Direction Play(Direction move)
        {
            return move;
        }

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
