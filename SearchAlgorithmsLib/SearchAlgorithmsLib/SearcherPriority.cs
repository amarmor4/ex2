using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;
namespace SearchAlgorithmsLib
{
    public abstract class SearcherPriority<T> : ISearcher<T>
    {
        private SimplePriorityQueue<State<T>> openList;
        private int evaluatedNodes;

        public int OpenListSize
        {
            get { return openList.Count; }
        }

        public SearcherPriority()
        {
            openList = new SimplePriorityQueue<State<T>>();
            evaluatedNodes = 0;
        }
        protected State<T> popOpenList()
        {
            updateEvaluatedCount();
            return openList.Dequeue();
        }
        protected void addToOpenList(State<T> state)
        {
            updateEvaluatedCount();
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
        
        public List<State<T>> createBackTrace(State<T> goalState)
        {
            Stack<State<T>> backTraceStack = new Stack<State<T>>();
            List<State<T>> backTrace = new List<State<T>>();
            State<T> current = goalState;
            while (current != null)
            {
                backTraceStack.Push(current);
                current = current.cameFrom;
            }
            while (backTraceStack.Count() != 0)
            {
                backTrace.Add(backTraceStack.Pop());
            }
            return backTrace;
        }

        public void updateEvaluatedCount()
        {
            this.evaluatedNodes++;
        }

        public abstract Solution<T> search(ISearchable<T> searchable);

        public int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        public void updateOpenList(State<T> current)
        {
            updateEvaluatedCount();
            this.openList.Remove(current);
            addToOpenList(current);
        }
    }
}
