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
            /*
            string json = @"{ 
                'Name': 'mymaze',
                'Maze': '0001010001010101011101010100011101011101000100010101111101010000000101011111110100000000011111111111',
                'Rows': 10,
                'Cols': 10,
                'Start':
                {
                'Row': 0,
                'Col': 4
                },
                'End':
                {
                'Row': 2,
                'Col': 8
                }
                } ";
            Maze maze = Maze.FromJSON(json);
            Console.WriteLine(maze.ToString());
            Adapter<Position, Direction> adapter = new MazeToSearchableAdapter<Position, Direction>(maze);
            ISearchable<Position> searchableMaze = new Searchable<Position, Direction>(adapter);
            ISearcher<Position> BFS = new Bfs<Position>();
            Solution<Position> solution = BFS.search(searchableMaze);
            List<Direction> directionSol = adapter.convertSolution(solution);
            foreach (Direction d in directionSol)
            {
                Console.Write((int)d);
            }
            Console.WriteLine();
            Console.WriteLine("BFS: " + BFS.getNumberOfNodesEvaluated());
           */
           /*
               IMazeGenerator mazeGenerator = new DFSMazeGenerator();
               Maze maze = mazeGenerator.Generate(60,60);
               Console.WriteLine(maze.ToString());
               Adapter<Position, Direction> adapter = new MazeToSearchableAdapter<Position, Direction>(maze);
               ISearchable<Position> searchableMaze = new Searchable<Position,Direction>(adapter);
               ISearcher<Position> BFS = new Bfs<Position>();
               Solution<Position> solution = BFS.search(searchableMaze);
               List<Direction> directionSol = adapter.convertSolution(solution);
            if (directionSol != null)
            {
                Console.Write("sol: ");
                foreach (Direction d in directionSol)
                {
                    Console.Write((int)d);
                }
            }
                Console.WriteLine();
           Console.WriteLine("BFS: " + BFS.getNumberOfNodesEvaluated());
           */
           /*
           ISearcher<Position> DFS = new Dfs<Position>();
           Solution<Position> solution1 = DFS.search(searchableMaze);
           List<Direction> directionSol1 = adapter.convertSolution(solution1);
           Console.WriteLine("DFS: " + DFS.getNumberOfNodesEvaluated());
           foreach (Direction d in directionSol1)
           {
               Console.Write((int)d);
           }*/
        }
        

        static void Main(string[] args)
        {
            //CompareSolvers();
            //Console.ReadKey();
            Controller c = new Controller();
            IClientHandler ch = new ClientHandler(c);
            Server server = new Server(8000, ch);
            c.setView(server);
            IModel m = new MazeModel(c);
            c.setModel(m);
            server.Start();
            Console.ReadKey();
        }
    }
}
