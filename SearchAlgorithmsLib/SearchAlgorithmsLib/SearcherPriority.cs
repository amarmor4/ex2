using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;
namespace SearchAlgorithmsLib
{
    /// <summary>
    /// implement Isearcher for algoritem (doesn't use priority queue).
    /// </summary>
    /// <typeparam name="T">type of state</typeparam>
    public abstract class SearcherPriority<T> : ISearcher<T>
    {
        /// <summary>
        /// queue of discovers vertex.
        /// </summary>
        private SimplePriorityQueue<State<T>> openList;

        /// <summary>
        /// number of nodes that algorithem evaluated.
        /// </summary>
        private int evaluatedNodes;

        /// <summary>
        /// the size of the openList queue
        /// </summary>
        public int OpenListSize
        {
            get { return openList.Count; }
        }

        /// <summary>
        /// constructor
        /// </summary>
        public SearcherPriority()
        {
            openList = new SimplePriorityQueue<State<T>>();
            evaluatedNodes = 0;
        }

        /// <summary>
        /// pop vertex from priority queue openList
        /// </summary>
        /// <returns>vertex with high cost in queue</returns>
        protected State<T> PopOpenList()
        {
            UpdateEvaluatedCount();
            return openList.Dequeue();
        }

        /// <summary>
        /// add vertex to openList queue
        /// </summary>
        /// <param name="state">vertex - state</param>
        protected void AddToOpenList(State<T> state)
        {
            this.openList.Enqueue(state, (float)state.Cost);
        }

        /// <summary>
        /// check if vertex contain at openList queue
        /// </summary>
        /// <param name="stateToFind">vertex - state to check</param>
        /// <returns></returns>
        protected bool OpenContaines(State<T> stateToFind)
        {
            foreach(State<T> s in this.openList)
            {
                if (s.Equals(stateToFind))
                    return true;
            }
                return false;
        }
        
        /// <summary>
        /// create list of vertex from initialize state to goal.
        /// </summary>
        /// <param name="goalState">goal vertex - state</param>
        /// <returns>list of states</returns>
        public List<State<T>> CreateBackTrace(State<T> goalState)
        {
            Stack<State<T>> backTraceStack = new Stack<State<T>>();
            List<State<T>> backTrace = new List<State<T>>();
            State<T> current = goalState;
            while (current != null)
            {
                backTraceStack.Push(current);
                current = current.CameFrom;
            }
            while (backTraceStack.Count() != 0)
            {
                backTrace.Add(backTraceStack.Pop());
            }
            return backTrace;
        }

        /// <summary>
        /// update counter of evaluated vertex.
        /// </summary>
        public void UpdateEvaluatedCount()
        {
            this.evaluatedNodes++;
        }

        /// <summary>
        /// find solution to search problem.
        /// </summary>
        /// <param name="searchable">problem</param>
        /// <returns>solution to problem</returns>
        public abstract Solution<T> Search(ISearchable<T> searchable);

        /// <summary>
        /// get the number of the nodes that evaluated.
        /// </summary>
        /// <returns>counter</returns>
        public int GetNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }

        /// <summary>
        /// update vertex in priority queue.
        /// </summary>
        /// <param name="current">vertex to update</param>
        public void UpdateOpenList(State<T> current)
        {
            this.openList.Remove(current);
            AddToOpenList(current);
        }
    }
}
