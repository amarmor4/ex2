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
    /// <summary>
    /// main of server side
    /// </summary>
    class Program
    {
        /// <summary>
        /// Roy's example. 
        /// </summary>
        public static void test()
        {
            string json = @"{
                'Name': 'mymaze',
                'Maze':
                '0001010001010101110101010000010111111101000001000111010101110001010001011111110100000000011111111111',
                'Rows': 10,
                'Cols': 10,
                'Start': {
                    'Row': 0,
                    'Col': 4
                },
                'End': {
                    'Row': 0,
                    'Col': 0
                }
            }";
            Maze maze = Maze.FromJSON(json);
            Console.Write(maze.ToString());
            Adapter<Position> adapter = new MazeToSearchableAdapter<Position>(maze);
            ISearchable<Position> searchableMaze = new Searchable<Position, Direction>(adapter);
            ISearcher<Position> bfs= new Bfs<Position>();
            ISearcher<Position> dfs= new Dfs<Position>();
            Solution<Position> solBfs = bfs.Search(searchableMaze);
            Solution<Position> solDfs = dfs.Search(searchableMaze);
            Console.WriteLine("bfs " + solBfs.NodesEvaluated.ToString());
            Console.WriteLine("dfs " + solDfs.NodesEvaluated);
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            //test();
            Controller c = new Controller();
            IClientHandler ch = new ClientHandler(c);
            Server server = new Server(ch);
            c.setView(ch);
            IModel m = new MazeModel(c);
            c.setModel(m);
            server.Start();
            Console.ReadKey();
        }
    }
}
