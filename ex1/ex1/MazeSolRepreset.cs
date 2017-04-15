using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace ex1
{
    class MazeSolRepreset: SolutionRepresent<MazeLib.Direction, MazeLib.Position, int>
    {
        string mazeName;
        Solution<MazeLib.Position> solution;
        List<int> solRepreset { get; set; }

        public MazeSolRepreset(Solution<MazeLib.Position> sol)
        {
            this.solution = sol;
            this.mazeName = sol.name;
            List<MazeLib.Direction> solRepreset = new List<MazeLib.Direction>();
            this.solRepreset=ConvertSolution();
        }

        public List<int> ConvertSolution()
        {
            if (this.solution != null)
            {
                List<int> temp = new List<int>();

                for (int i = 0; i < this.solution.backTrace.Count - 1; i++)
                {
                    temp.Add((int)ComperTo(this.solution.backTrace[i], this.solution.backTrace[i + 1]));
                }
                return temp;
            }
            return null;
        }

        public MazeLib.Direction ComperTo(State<MazeLib.Position> current, State<MazeLib.Position> to)
        {
            if (current.state.Row == to.state.Row)
            {
                if (to.state.Col == current.state.Col + 1)
                    return MazeLib.Direction.Right;
                if (to.state.Col == current.state.Col - 1)
                    return MazeLib.Direction.Left;
            }
            if (current.state.Col == to.state.Col)
            {
                if (to.state.Row == current.state.Row + 1)
                    return MazeLib.Direction.Down;
                if (to.state.Row == current.state.Row - 1)
                    return MazeLib.Direction.Up;
            }
            return MazeLib.Direction.Unknown;
        }

        public string ToJSON()
        {
            JObject mazeSolObj = new JObject();
            mazeSolObj["Name"] = this.mazeName;
            mazeSolObj["Solution"] = string.Join("", this.solRepreset);
            mazeSolObj["NodesEvaluated"] = this.solution.nodesEvaluated;
            return mazeSolObj.ToString();
        }
    }
}
