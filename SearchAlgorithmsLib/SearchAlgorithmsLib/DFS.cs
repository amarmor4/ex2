using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class Dfs<T> : Searcher<T>
    {
        public override Solution search(ISearchable<T> searchable)
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
                        pushToStack(neg);
                    }
                }
            }
            return null; //TODO build solution
        }

    }
}
