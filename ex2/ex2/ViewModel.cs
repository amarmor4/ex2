using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ex2
{
    /// <summary>
    /// viewmodle abstract class
    /// </summary>
    abstract class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// event PropertyChangedEventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// notify property changed
        /// </summary>
        /// <param name="propName">Property name that change</param>
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
