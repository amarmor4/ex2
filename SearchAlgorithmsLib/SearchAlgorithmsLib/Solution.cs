using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class Solution<T>
    {
        public List<State<T>> backTrace
        {
            get; set;
        }

        public Solution(List<State<T>> trace)
        {
            this.backTrace = trace;
        }

    }
}
