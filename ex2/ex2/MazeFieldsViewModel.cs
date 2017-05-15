using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex2
{
    /// <summary>
    /// maze fields viewModel
    /// </summary>
    class MazeFieldsViewModel :ViewModel
    {
        /// <summary>
        /// settings model.
        /// </summary>
        private IMazeFieldsModel model;

        /// <summary>
        /// constractor.
        /// </summary>
        /// <param name="model">settings model</param>
        public MazeFieldsViewModel(IMazeFieldsModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// maze rows property
        /// </summary>
        public int MazeRows
        {
            get { return model.MazeRows; }
            set
            {
                model.MazeRows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }

        /// <summary>
        /// maze cols property
        /// </summary>
        public int MazeCols
        {
            get { return model.MazeCols; }
            set
            {
                model.MazeCols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }
    }
}
