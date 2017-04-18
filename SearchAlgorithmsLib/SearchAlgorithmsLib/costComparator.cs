using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    class costComparator<T>:Icomparator<T>
    {
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
