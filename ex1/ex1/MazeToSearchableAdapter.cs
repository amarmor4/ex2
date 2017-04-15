using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;

namespace ex1
{
    class MazeToSearchableAdapter<Position> : Adapter<MazeLib.Position>
    {
        private Maze adapteeMaze;

        public MazeToSearchableAdapter(Maze myMaze)
        {
            this.adapteeMaze = myMaze;
        }

        public State<MazeLib.Position> getIntialized()
        {
            State<MazeLib.Position> start = State<MazeLib.Position>.StatePool.getState(adapteeMaze.InitialPos);
            return start;

        }

        public State<MazeLib.Position> getGoal()
        {
            State<MazeLib.Position> goal = State<MazeLib.Position>.StatePool.getState(this.adapteeMaze.GoalPos);
            return goal;
        }

        public string GetName()
        {
            return this.adapteeMaze.Name;
        }

        private State<MazeLib.Position> createPos (int row, int col)
        {
            MazeLib.Position tempPos = new MazeLib.Position(row, col);
            State<MazeLib.Position> tempState = State<MazeLib.Position>.StatePool.getState(tempPos);
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

       
        public double costBetNeg(State<MazeLib.Position> neg1 , State<MazeLib.Position> neg2)
        {
            return 1;
        }

        public void updateParent(State<MazeLib.Position> current, State<MazeLib.Position> parent)
        {
            current.cameFrom = parent;
            current.cost = parent.cost + costBetNeg(current, parent);
        }
    }
}
