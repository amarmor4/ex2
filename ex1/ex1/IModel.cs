using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace ex1
{
    interface IModel
    {
        Maze Generate(string name, int rows, int cols);
        Solution<Position> Solve(string name, int algo);
        Maze Start(string name, int rows, int cols);
        List<string> List();
        Maze Join(string name);
        Direction Play(MazeLib.Direction move);
        bool Close(string name);
    }
}
