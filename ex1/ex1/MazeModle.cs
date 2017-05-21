using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;
using MazeGeneratorLib;
using System.Net.Sockets;

namespace ex1
{
    /// <summary>
    /// implelemt model of maze.
    /// </summary>
    class MazeModel : IModel
    {
        /// <summary>
        /// pool of solution - singal player.
        /// </summary>
        Dictionary<string, Solution<Position>> solutionsSinglePlayerPool;

        /// <summary>
        /// pool of mazes - singal player.
        /// </summary>
        Dictionary<string, Maze> mazesSinglePlayerPool;

        /// <summary>
        /// pool of Game - multi player.
        /// </summary>
        //Dictionary<string, Game> mazesMultiPlayerPool;
        Dictionary<string, Game> multiPlayersGames;

        Dictionary<TcpClient, Game> clientsAtGame;

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
            this.solutionsSinglePlayerPool = new Dictionary<string, Solution<Position>>();
            this.mazesSinglePlayerPool = new Dictionary<string, Maze>();
            //this.mazesMultiPlayerPool = new Dictionary<string, Maze>();
            this.multiPlayersGames = new Dictionary<string, Game>();
            this.clientsAtGame = new  Dictionary<TcpClient, Game>();
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
            if (!this.mazesSinglePlayerPool.ContainsKey(name))
            {
                this.mazesSinglePlayerPool.Add(name, maze);
            }
            else
            {
                this.mazesSinglePlayerPool[name] = maze;
                if (this.solutionsSinglePlayerPool.ContainsKey(name))
                    this.solutionsSinglePlayerPool.Remove(name);
                //Console.WriteLine("previous maze with the same name overrided");
            }
            return maze;
        }

        /// <summary>
        /// solve the maze problem - singal player.
        /// </summary>
        /// <param name="name">maze name</param>
        /// <param name="algo">0-bfs, 1-dfs</param>
        /// <returns>get solution of maze problem</returns>
        public Solution<Position> Solve(string name, int algo)
        {
            if (this.mazesSinglePlayerPool.ContainsKey(name))
            {
                if (!this.solutionsSinglePlayerPool.ContainsKey(name))
                {
                    ISearcher<Position> searchAlgo;
                    Solution<Position> solution;
                    Maze maze = this.mazesSinglePlayerPool[name];
                    Adapter<Position> adapter = new MazeToSearchableAdapter<Position>(maze);
                    ISearchable<Position> searchableMaze = new Searchable<Position, Direction>(adapter);
                    switch (algo)
                    {
                        case 0:
                            searchAlgo = new Bfs<Position>();
                            break;
                        case 1:
                            searchAlgo = new Dfs<Position>();
                            break;
                        default:
                            //Error at algorithem numeber: 0 - for bfs, 1 - for dfs
                            return null;
                    }
                    solution = searchAlgo.Search(searchableMaze);
                    this.solutionsSinglePlayerPool.Add(name, solution);
                }
                return this.solutionsSinglePlayerPool[name];
            }
            //name of maze doesn't exist at maze single player pool"
            return null;
        }

        /// <summary>
        /// Generate a maze for two players.
        /// </summary>
        /// <param name="name">maze name</param>
        /// <param name="rows">number of rows at maze.</param>
        /// <param name="cols">number of cols at maze.</param>
        /// <returns>maze</returns>
        public string Start(string name, int rows, int cols, TcpClient client)
        {
                Maze maze = GenerateMultiPlayresMaze(name, rows, cols);
                if (maze == null)
                    return null;
                this.gamesToJoin.Add(name);
                Game game = new Game(name, maze, client);
                this.multiPlayersGames.Add(name, game);
                this.clientsAtGame.Add(client, game);
                //game.WaitToAnotherPlayer();
                return "wait for another player";
        }

        private Maze GenerateMultiPlayresMaze(string name, int rows, int cols)
        {
            Maze maze;
            if(!this.multiPlayersGames.ContainsKey(name))
            {
                IMazeGenerator mazeGenerator = new DFSMazeGenerator();
                maze = mazeGenerator.Generate(rows, cols);
                maze.Name = name;
            }
            else
            {
                //"Error: exist maze with the same name at multiplayer pool"
                return null;
            }
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
        public Maze Join(string name, TcpClient client)
        {
            if (!this.gamesToJoin.Contains(name))
                //"Error: game doesn't exist in list games to join"
                return null;
            
            this.gamesToJoin.Remove(name);
            if (this.multiPlayersGames.ContainsKey(name))
            {
                Game game=this.multiPlayersGames[name];
                if (game.AddSecondPlayer(client))
                {
                    this.clientsAtGame.Add(client, game);
                    return game.Maze;
                }
            }
            return null;
        }



        /// <summary>
        /// play one move, in two playres game.
        /// </summary>
        /// <param name="move">direction of player at maze.</param>
        /// <returns>the move</returns>
        public string Play(Direction move, TcpClient client)
        {
            Game game = this.clientsAtGame[client];
            return game.MoveToJSON(move);  
        }

        /// <summary>
        /// close the maze between to playres.
        /// </summary>
        /// <param name="name">maze name</param>
        /// <returns>true - Succeeded in close</returns>
        public string Close(string name, TcpClient client)
        {
            Game game = this.clientsAtGame[client];
            if (game.Name == name)
            {
                if (this.gamesToJoin.Contains(name))
                    this.gamesToJoin.Remove(name);
                game.DisConnectPlayer(client);
                this.clientsAtGame.Remove(client);
                TcpClient otherPlayer = game.GetOtherPlayer(client);
                if (otherPlayer != null)
                {
                    this.clientsAtGame.Remove(otherPlayer);
                    this.multiPlayersGames.Remove(name);
                }
                return null;
            }
            return "Error try to close not exist game or game of others player";
        }

        /// <summary>
        /// check if client participate in multiplayer game.
        /// </summary>
        /// <param name="client">client</param>
        /// <returns>true if participate, otherwise not</returns>
        public bool IsParticipate(TcpClient client)
        {
            if (!this.clientsAtGame.ContainsKey(client))
                //"Error: client don't Participating in multi player game"
                return false;
            
            return true;
        }

        /// <summary>
        /// get other participate at game.
        /// </summary>
        /// <param name="client">client</param>
        /// <returns>other participate at game</returns>
        public TcpClient GetOtherParticipate(TcpClient client)
        {
            if (IsParticipate(client))
            {
                Game game = this.clientsAtGame[client];
                TcpClient otherParticipate=game.GetOtherPlayer(client);
                return otherParticipate;
            }
            return null;
        }
    }
}
