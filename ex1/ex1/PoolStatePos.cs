using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;

namespace ex1
{
    public class PoolStatePos : PoolState<MazeLib.Position>
    {
        public override State<MazeLib.Position> create(MazeLib.Position obj)
        {
            return new State<MazeLib.Position>(new MazeLib.Position(1,2));
        }
    }
}
