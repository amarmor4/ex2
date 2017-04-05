using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;
namespace SearchAlgorithmsLib
{
    public abstract class Searcher<T> : ISearcher<T>
    {
        private SimplePriorityQueue<State<T>> openList;
        private int evaluatedNodes;
        public int OpenListSize
        {
            get { return openList.Count; }
        }

        public Searcher()
        {
            openList = new SimplePriorityQueue<State<T>>();
            evaluatedNodes = 0;
        }
        protected State<T> popOpenList()
        {
            evaluatedNodes++;
            return openList.Dequeue();
        }
        protected void addToOpenList(State<T> state)
        {
            this.openList.Enqueue(state, (float)state.cost);
        }
        protected bool openContaines(State<T> stateToFind)
        {
            foreach(State<T> s in this.openList)
            {
                if (s.Equals(stateToFind))
                    return true;
            }
                return false;
        }

        public abstract Solution search(ISearchable<T> searchable);
        public int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }
    }
}
