using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;

namespace ex1
{
    class MazeToSearchableAdapter<Position, Direction> : Adapter<MazeLib.Position, MazeLib.Direction>
    {
        private Maze adapteeMaze;

        public MazeToSearchableAdapter(Maze myMaze)
        {
            this.adapteeMaze = myMaze;
        }

        public State<MazeLib.Position> getIntialized()
        {
            State<MazeLib.Position> start = new State<MazeLib.Position>(adapteeMaze.InitialPos);
            return start;

        }

        public State<MazeLib.Position> getGoal()
        {
            State<MazeLib.Position> goal = new State<MazeLib.Position>(this.adapteeMaze.GoalPos);
            return goal;
        }

        private State<MazeLib.Position> createPos (int row, int col)
        {
            MazeLib.Position tempPos = new MazeLib.Position(row, col);
            State<MazeLib.Position> tempState = new State<MazeLib.Position>(tempPos);
            return tempState;
        }

        public List<State<MazeLib.Position>> getAllPossible(State<MazeLib.Position> current)
        {
            List<State<MazeLib.Position>> temp = new List<State<MazeLib.Position>>();
            int row = current.state.Row;
            int col = current.state.Col;

            if (col >= 0 && col <= this.adapteeMaze.Cols - 1)
            {
                if ((row - 1) >= 0)
                {
                    if (this.adapteeMaze[row - 1, col] == 0)
                        temp.Add(createPos(row - 1, col));
                }
                if ((row + 1) <= this.adapteeMaze.Rows - 1)
                {
                    if (this.adapteeMaze[row + 1, col] == 0)
                        temp.Add(createPos(row + 1, col));
                }
            }

            if (row >= 0 && row <= this.adapteeMaze.Rows - 1)
            {
                if ((col - 1) >= 0)
                {
                    if (this.adapteeMaze[row, col - 1] == 0)
                        temp.Add(createPos(row, col - 1));
                }
                if ((col + 1) <= this.adapteeMaze.Cols - 1)
                {
                    if (this.adapteeMaze[row, col + 1] == 0)
                        temp.Add(createPos(row, col + 1));
                }
            }
            return temp;
        }

        //TODO
        public double costBetNeg(State<MazeLib.Position> neg1 , State<MazeLib.Position> neg2)
        {
            return 0;
        }

        public void updateParent(State<MazeLib.Position> current, State<MazeLib.Position> parent)
        {
            current.cameFrom = parent;
            current.cost = parent.cost + costBetNeg(current, parent);
        }

        private MazeLib.Direction comperTo(State<MazeLib.Position> current, State<MazeLib.Position> to)
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

        public List<MazeLib.Direction> convertSolution(Solution<MazeLib.Position> solution)
        {
            List<MazeLib.Direction> temp = new List<MazeLib.Direction>();
            
            for(int i = 0; i < solution.backTrace.Count - 1; i++)
            {
                temp.Add(comperTo(solution.backTrace[i], solution.backTrace[i + 1]));
            }
            return temp;
        }
    }
}
