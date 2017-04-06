using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;

namespace ex1
{
    class MazeToSearchableAdapter
    {
        private Maze adapteeMaze;

        public MazeToSearchableAdapter(Maze myMaze)
        {
            this.adapteeMaze = myMaze;
        }

        public State<Position> getIntialized()
        {
            State<Position> start = new State<Position>(this.adapteeMaze.InitialPos);
            return start;

        }

        public State<Position> getGoal()
        {
            State<Position> goal = new State<Position>(this.adapteeMaze.GoalPos);
            return goal;
        }

        public List<State<Position>> getAllPossible(State<Position> current)
        {
            return new List<State<Position>>();

        }
        //comment
    }
}
