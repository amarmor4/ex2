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
        private Adapter<T> adapter;

        public Searchable(Adapter<T> objectAdapter)
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

        public string GetName()
        {
            return this.adapter.GetName();
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
