using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// interface comparator
    /// </summary>
    interface Icomparator<T>
    {
        /// <summary>
        /// compate betwwen to states.
        /// </summary>
        /// <param name="stateOne">state1</param>
        /// <param name="stateTwo">state2</param>
        /// <returns>0-if equales, 1 if state1 bitter than state2,
        /// -1 if state1 smaller than state2</returns>
        int Compare(State<T> stateOwn, State<T> stateTwo);
    }
}
