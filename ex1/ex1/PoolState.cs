using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;

namespace ex1
{
    public abstract class PoolState<T>
    {
        protected State<T> temp;
        protected HashSet<State<T>> pool;

        public PoolState()
        {
            this.pool = new HashSet<State<T>>();
        }

        public abstract State<T> create(T obj);
    }
}
