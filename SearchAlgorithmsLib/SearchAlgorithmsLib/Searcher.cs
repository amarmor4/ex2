using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// implement Isearcher for algoritem that need to use priority queue
    /// </summary>
    /// <typeparam name="T">type of state</typeparam>
    public abstract class Searcher<T> : ISearcher<T>
    {
        /// <summary>
        /// stack of discoverd vertex.
        /// </summary>
        private Stack<State<T>> stack;

        /// <summary>
        /// number of nodes that algorithem evaluated.
        /// </summary>
        private int evaluatedNodes;

        /// <summary>
        /// constructor
        /// </summary>
        public Searcher()
        {
            this.stack = new Stack<State<T>>();
            this.evaluatedNodes = 0;
        }

        /// <summary>
        /// update counter of evaluated vertex.
        /// </summary>
        public void UpdateEvaluatedCount()
        {
            this.evaluatedNodes++;
        }

        /// <summary>
        /// pop vertex from stack.
        /// </summary>
        /// <returns>pop vertex</returns>
        public State<T> PopFromStack()
        {
            UpdateEvaluatedCount();
            return this.stack.Pop();
        }

        /// <summary>
        /// push vertex to stack.
        /// </summary>
        /// <param name="s">vertex to push</param>
        public void PushToStack(State<T> s)
        {
            this.stack.Push(s);
        }

        /// <summary>
        /// check if stack is empty.
        /// </summary>
        /// <returns>true if empty, otherwise false</returns>
        public bool StackIsEmpty()
        {
            return this.stack.Count == 0;
        }

        /// <summary>
        /// create list of vertex from initialize state to goal.
        /// </summary>
        /// <param name="discoverde">goal vertex - state</param>
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
            //return discoverde.ToList<State<T>>();
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
            return this.evaluatedNodes;
        }
    }
}
