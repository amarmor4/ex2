using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public interface ISearchable<T>
    {
        State<T> getInitialState();
        State<T> getGoalState();
        List<State<T>> getAllPossibleStates(State<T> s);
        //0-no neighbor, Weight between two vertex
        double getDistanceNeg(State<T> v, State<T> u);
        /*s.cameFrom = n;
        s.cost = n.cost + searchable.getDistanceNeg(n, s);*/
        void updateState(State<T> current, State<T> parent);
    }
}
