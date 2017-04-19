using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// implemente for cost comparator
    /// </summary>
    class costComparator<T>:Icomparator<T>
    {
        /// <summary>
        /// compate betwwen to states.
        /// </summary>
        /// <param name="stateOne">state1</param>
        /// <param name="stateTwo">state2</param>
        /// <returns>0-if equales, 1 if state1 bitter than state2,
        /// -1 if state1 smaller than state2</returns>
        public int Compare(State<T> stateOne, State<T> stateTwo)
        {
            if (stateOne.Cost > stateTwo.Cost)
                return 1;
            else if (stateTwo.Cost > stateOne.Cost)
                return -1;
            else
                return 0;
        }
    }
}
