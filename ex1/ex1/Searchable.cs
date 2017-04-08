using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;

namespace ex1
{
    class Searchable<T,T1> : ISearchable<T>
    {
        private Adapter<T,T1> adapter;

        public Searchable(Adapter<T,T1> objectAdapter)
        {
            this.adapter = objectAdapter;
        }

        public State<T> getInitialState()
        {
            return this.adapter.getIntialized();
        }

        public State<T> getGoalState()
        {
            return this.adapter.getGoal();
        }

        public List<State<T>> getAllPossibleStates(State<T> current)
        {
            return this.adapter.getAllPossible(current);
        }

        public double getDistanceNeg(State<T> neg1, State<T> neg2)
        {
            return this.adapter.costBetNeg(neg1, neg2);
        }

        public void updateState(State<T> current, State<T> parent)
        {
            this.adapter.updateParent(current, parent);
        }
    }
}
