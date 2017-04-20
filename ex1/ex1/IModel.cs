using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using System.Net.Sockets;

namespace ex1
{
    /// <summary>
    /// model interface of maze.
    /// </summary>
    interface IModel
    {
        /// <summary>
        /// Generate a maze for single player.
        /// </summary>
        /// <param name="name">maze name</param>
        /// <param name="rows">number of rows at maze.</param>
        /// <param name="cols">number of cols at maze.</param>
        /// <returns>maze</returns>
        Maze Generate(string name, int rows, int cols);

        /// <summary>
        /// solve the maze problem.
        /// </summary>
        /// <param name="name">maze name</param>
        /// <param name="algo">0-bfs, 1-dfs</param>
        /// <returns>get solution of maze problem</returns>
        Solution<Position> Solve(string name, int algo);

        /// <summary>
        /// Generate a maze for two players.
        /// </summary>
        /// <param name="name">maze name</param>
        /// <param name="rows">number of rows at maze.</param>
        /// <param name="cols">number of cols at maze.</param>
        /// <returns>maze</returns>
        String Start(string name, int rows, int cols, TcpClient client);

        /// <summary>
        /// list of games that can join - two players.
        /// </summary>
        /// <returns>list of games that can join</returns>
        List<string> List();

        /// <summary>
        /// join to game of two players.
        /// </summary>
        /// <param name="name">maze name</param>
        /// <returns>maze</returns>
        Maze Join(string name, TcpClient client);

        /// <summary>
        /// play one move, in two playres game.
        /// </summary>
        /// <param name="move">direction of player at maze.</param>
        /// <returns>the move</returns>
        string Play(MazeLib.Direction move, TcpClient client);

        /// <summary>
        /// close the maze between to playres.
        /// </summary>
        /// <param name="name">maze name</param>
        /// <returns>true - Succeeded in close</returns>
        string Close(string name, TcpClient client);

        /// <summary>
        /// get other participate at game.
        /// </summary>
        /// <param name="client">client</param>
        /// <returns>other participate at game</returns>
        TcpClient GetOtherParticipate(TcpClient client);

        /// <summary>
        /// check if client participate in multiplayer game.
        /// </summary>
        /// <param name="client">client</param>
        /// <returns>true if participate, otherwise not</returns>
        bool IsParticipate(TcpClient client);
    }
}
