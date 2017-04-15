using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// solve the search problem with dfs algorithem.
    /// </summary>
    /// <typeparam name="T">search problem</typeparam>
    public class Dfs<T> : Searcher<T>
    {
        /// <summary>
        /// find solution to search problem.
        /// </summary>
        /// <param name="searchable">problem</param>
        /// <returns>solution to problem</returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            PushToStack(searchable.GetInitialState());
            HashSet<State<T>> discovered = new HashSet<State<T>>();

            while (!StackIsEmpty())
            {
                State<T> current = PopFromStack();
                if (current == searchable.GetGoalState())
                {
                    return new Solution<T>(CreateBackTrace(discovered), GetNumberOfNodesEvaluated(), searchable.GetName());
                }
                if (!discovered.Contains(current))
                {
                    discovered.Add(current);
                    foreach (State<T> neg in searchable.GetAllPossibleStates(current))
                    {
                        if(!discovered.Contains(neg))
                            PushToStack(neg);
                    }
                }
            }
            return new Solution<T>(CreateBackTrace(discovered), GetNumberOfNodesEvaluated(), searchable.GetName());
        }

    }
}
