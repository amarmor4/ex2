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
        int Compare(State<T> stateOwn, State<T> stateTwo);
    }
}
