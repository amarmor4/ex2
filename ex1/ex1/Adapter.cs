using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;

namespace ex1
{
    interface Adapter<T1, T2>
    {
        State<T1> getIntialized();

        State<T1> getGoal();

        List<State<T1>> getAllPossible(State<T1> current);

        double costBetNeg(State<T1> neg1, State<T1> neg2);

        void updateParent(State<T1> current, State<T1> parent);

        List<T2> convertSolution(Solution<T1> solution);
    }
}
