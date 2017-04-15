using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// serach problem.
    /// </summary>
    /// <typeparam name="T">type of serach problem</typeparam>
    public interface ISearchable<T>
    {
        /// <summary>
        /// name of serach problem
        /// </summary>
        /// <returns>name</returns>
        string GetName();

        /// <summary>
        /// get initial state - vertex.
        /// </summary>
        /// <returns>initial state</returns>
        State<T> GetInitialState();

        /// <summary>
        /// get goal state - vertex.
        /// </summary>
        /// <returns>goal state</returns>
        State<T> GetGoalState();

        /// <summary>
        /// get all possible neighbors.
        /// </summary>
        /// <param name="s">state to check is neighbors</param>
        /// <returns>list of neighbors state - vertex </returns>
        List<State<T>> GetAllPossibleStates(State<T> s);

        /// <summary>
        /// get cost between neighbors.
        /// 0-no neighbor, Weight between two vertex
        /// </summary>
        /// <param name="v">one state - vertex</param>
        /// <param name="u">seconde state - vertex</param>
        /// <returns>distance between neighbors</returns>
        double GetCostNeg(State<T> v, State<T> u);

        /// <summary>
        /// update state.
        /// s.cameFrom = n; s.cost = n.cost + searchable.getDistanceNeg(n, s);
        /// </summary>
        /// <param name="current">state to update.</param>
        /// <param name="parent">came from - parent</param>
        void UpdateState(State<T> current, State<T> parent);
    }
}
