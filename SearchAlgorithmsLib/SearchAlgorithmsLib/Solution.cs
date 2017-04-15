using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class Solution<T>
    {
        public List<State<T>> backTrace { get; set; }
        public int nodesEvaluated { get; set; }
        public string name { get; set; }

        public Solution(List<State<T>> trace, int evaluatedNodes, string searcherName)
        {
            this.backTrace = trace;
            this.nodesEvaluated = evaluatedNodes;
            this.name = searcherName;
        }
    }
}
