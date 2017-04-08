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

        public State(T myState){
            this.state = myState;
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

    }
}
