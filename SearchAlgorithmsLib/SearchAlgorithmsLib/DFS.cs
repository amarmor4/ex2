using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class Dfs<T> : Searcher<T>
    {
        public override Solution<T> search(ISearchable<T> searchable)
        {
            pushToStack(searchable.getInitialState());
            HashSet<State<T>> discovered = new HashSet<State<T>>();

            while (!stackIsEmpty())
            {
                State<T> current = popFromStack();
                if (!discovered.Contains(current))
                {
                    discovered.Add(current);
                    updateEvaluatedCount();
                    foreach (State<T> neg in searchable.getAllPossibleStates(current))
                    {
                        if(!discovered.Contains(neg))
                            pushToStack(neg);
                    }
                }
            }
            return new Solution<T>(createBackTrace(discovered), getNumberOfNodesEvaluated(), searchable.GetName());
        }

    }
}
