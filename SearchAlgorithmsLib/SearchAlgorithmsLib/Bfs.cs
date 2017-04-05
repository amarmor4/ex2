using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class Bfs<T> : Searcher<T>
    {
        public override Solution search(ISearchable<T> searchable)
        {
            addToOpenList(searchable.getInitialState());
            HashSet<State<T>> closed = new HashSet<State<T>>();
            while (OpenListSize > 0)
            {
                State<T> current = popOpenList();
                closed.Add(current);
                if (current.Equals(searchable.getGoalState()))
                    return null; //TODO back trace of n in solution type
                List<State<T>> succerssors = searchable.getAllPossibleStates(current);
                foreach (State<T> neg in succerssors)
                {
                    if (!closed.Contains(neg) && !openContaines(neg))
                    {
                        searchable.updateState(neg, current);
                        addToOpenList(neg);
                    }
                    else
                    {
                        if (current.cost + searchable.getDistanceNeg(current, neg) < neg.cost)
                        {
                            if (!openContaines(neg))
                                addToOpenList(neg);
                            else
                            {
                                searchable.updateState(neg, current);
                            }
                        }
                    }
                }   
                  
            }
            return null;
        }
    }
}
