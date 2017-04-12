using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class State<T>
    {
        public T state{ get; set; }
        public double cost { get; set;}
        public State<T> cameFrom { get; set; }

        protected State(T myState){
            this.state = myState;
            this.cameFrom = null;
        }
        public bool Equals(State<T> s)
        {
            return (state.Equals(s.state));
        }

        public override int GetHashCode()
        {
            return this.state.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as State<T>);
        }

        public static class StatePool
        {
            static Dictionary<int, State<T>> pool = new Dictionary<int, State<T>>();
            static HashSet<T> hashT = new HashSet<T>();

            public static State<T> getState(T type)
            {
                if (!hashT.Contains(type))
                {
                    hashT.Add(type);
                    if (!pool.ContainsKey(type.ToString().GetHashCode()))
                    {
                        State<T> current = new State<T>(type);
                        pool.Add(type.ToString().GetHashCode(), current);
                    }
                }
                return pool[type.ToString().GetHashCode()];
            }
        }
    }
}
