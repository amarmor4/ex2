using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher<T> : ISearcher<T>
    {
        private Stack<State<T>> stack;
        private int evaluatedNodes;

        public Searcher()
        {
            this.stack = new Stack<State<T>>();
            this.evaluatedNodes = 0;
        }

        public void updateEvaluatedCount()
        {
            this.evaluatedNodes++;
        }

        public State<T> popFromStack()
        {
            return this.stack.Pop();
        }
        public void pushToStack(State<T> s)
        {
            this.stack.Push(s);
        }

        public bool stackIsEmpty()
        {
            return this.stack.Count == 0;
        }

        public abstract Solution search(ISearchable<T> searchable);
        public int getNumberOfNodesEvaluated()
        {
            return this.evaluatedNodes;
        }
    }
}
