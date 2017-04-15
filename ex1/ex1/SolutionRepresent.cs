using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
namespace ex1
{
    interface SolutionRepresent<T1,T2,T3>
    {
        List<T3> ConvertSolution();
        T1 ComperTo(State<T2> current, State<T2> to);
        string ToJSON();
    }
}
