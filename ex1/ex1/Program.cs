using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;
using MazeGeneratorLib;

namespace ex1
{
    class Program
    {

        public static void CompareSolvers()
        {
               IMazeGenerator mazeGenerator = new DFSMazeGenerator();
               Maze maze = mazeGenerator.Generate(10,10);
               Console.WriteLine(maze.ToString());

               Adapter<Position, Direction> adapter = new MazeToSearchableAdapter<Position, Direction>(maze);
               ISearchable<Position> searchableMaze = new Searchable<Position,Direction>(adapter);


               ISearcher<Position> BFS = new Bfs<Position>();
               Solution<Position> solution = BFS.search(searchableMaze);
               List<Direction> directionSol = adapter.convertSolution(solution);

               /*
               foreach(Direction d in directionSol)
               {
                   Console.Write(d);
               }*/

               ISearcher<Position> DFS = new Dfs<Position>();
               Solution<Position> solution1 = DFS.search(searchableMaze);
               List<Direction> directionSol1 = adapter.convertSolution(solution1);

               Console.WriteLine("BFS: " + BFS.getNumberOfNodesEvaluated());
               Console.WriteLine("DFS: " + DFS.getNumberOfNodesEvaluated());
               /*
               foreach (Direction d in directionSol1)
               {
                   Console.Write(d);
               }*/
        }

        static void Main(string[] args)
        {
            CompareSolvers();
            Console.ReadKey();
        }
    }
}
