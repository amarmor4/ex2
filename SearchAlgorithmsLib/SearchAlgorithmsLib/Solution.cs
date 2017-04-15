using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// solution for search problem.
    /// </summary>
    /// <typeparam name="T">type of state</typeparam>
    public class Solution<T>
    {
        /// <summary>
        /// list of vertex from initialize state to goal.
        /// </summary>
        public List<State<T>> BackTrace { get; set; }

        /// <summary>
        /// number of nodes that algorithem evaluated.
        /// </summary>
        public int NodesEvaluated { get; set; }

        /// <summary>
        /// name of the searcher problem
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="trace">back trace</param>
        /// <param name="evaluatedNodes">evaluated nodes</param>
        /// <param name="searcherName">name of the searcher problem</param>
        public Solution(List<State<T>> trace, int evaluatedNodes, string searcherName)
        {
            this.BackTrace = trace;
            this.NodesEvaluated = evaluatedNodes;
            this.Name = searcherName;
        }
    }
}
