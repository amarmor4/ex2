using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// state - vertex at searcher problem.
    /// </summary>
    /// <typeparam name="T">they of state</typeparam>
    public class State<T>
    {
        /// <summary>
        /// state in type T
        /// </summary>
        public T state{ get; set; }

        /// <summary>
        /// cost of vertex - state.
        /// </summary>
        public double Cost { get; set;}

        /// <summary>
        /// parent of vertex - came from state.
        /// </summary>
        public State<T> CameFrom { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="myState">vertex - state</param>
        protected State(T myState){
            this.state = myState;
            this.CameFrom = null;
        }

        /// <summary>
        /// check if vertex - state is equals.
        /// </summary>
        /// <param name="s">vertex - stste to check</param>
        /// <returns>true if equals, otherwise false</returns>
        public bool Equals(State<T> s)
        {
            return (state.Equals(s.state));
        }

        /// <summary>
        /// get hash code of object.
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return this.state.GetHashCode();
        }

        /// <summary>
        /// check if vertex - state is equals.
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as State<T>);
        }

        /// <summary>
        /// state pool class.
        /// </summary>
        public static class StatePool
        {
            /// <summary>
            /// dictionay of (hash, state-vertex)
            /// </summary>
            static Dictionary<int, State<T>> pool = new Dictionary<int, State<T>>();
            /// <summary>
            /// hash set of type T of state.
            /// </summary>
            static HashSet<T> hashT = new HashSet<T>();

            /// <summary>
            /// get state, when call function, check if state exist in dictionary,
            /// yes - return a reference to object, else - create one and add him to pool. 
            /// </summary>
            /// <param name="type">type T of state</param>
            /// <returns>vertex - state</returns>
            public static State<T> GetState(T type)
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
