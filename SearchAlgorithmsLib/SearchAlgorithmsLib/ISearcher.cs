using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// interface for searcher.
    /// </summary>
    /// <typeparam name="T">type of state</typeparam>
    public interface ISearcher<T>
    {
        /// <summary>
        /// find solution to search problem.
        /// </summary>
        /// <param name="searchable">problem</param>
        /// <returns>solution to problem</returns>
        Solution<T> Search(ISearchable<T> searchable);

        /// <summary>
        /// get the number of the nodes that evaluated.
        /// </summary>
        /// <returns>counter</returns>
        int GetNumberOfNodesEvaluated();

        /// <summary>
        /// update counter of evaluated vertex.
        /// </summary>
        void UpdateEvaluatedCount();
    }
}
