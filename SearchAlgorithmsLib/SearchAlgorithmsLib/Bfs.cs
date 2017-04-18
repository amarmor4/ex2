using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// solve the search problem with bfs algorithem.
    /// </summary>
    /// <typeparam name="T">search problem</typeparam>
    public class Bfs<T> : SearcherPriority<T>
    {
        /// <summary>
        /// find solution to search problem.
        /// </summary>
        /// <param name="searchable">problem</param>
        /// <returns>solution to problem</returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            State<T>.StatePool.ResetPoolState();
            AddToOpenList(searchable.GetInitialState());
            HashSet<State<T>> closed = new HashSet<State<T>>();
            while (OpenListSize > 0)
            {
                State<T> current = PopOpenList();
                closed.Add(current);
                if (current.Equals(searchable.GetGoalState()))
                {
                    return new Solution<T>(CreateBackTrace(current), GetNumberOfNodesEvaluated(), searchable.GetName());
                }
                List<State<T>> succerssors = searchable.GetAllPossibleStates(current);
                foreach (State<T> neg in succerssors)
                {
                    if (!closed.Contains(neg) && !OpenContaines(neg))
                    {
                        searchable.UpdateState(neg, current);
                        AddToOpenList(neg);
                    }
                    else
                    {
                        if (current.Cost + searchable.GetCostNeg(current, neg) < neg.Cost)
                        {
                            if (!OpenContaines(neg))
                                AddToOpenList(neg);
                            else
                            {
                                searchable.UpdateState(neg, current);
                                UpdateOpenList(neg);
                            }
                        }
                    }
                }                     
            }
            return null;
        }
    }
}
