using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex2
{
    /// <summary>
    /// application maze fields model
    /// </summary>
    class ApplicationMazeFieldsModel :IMazeFieldsModel
    {
        /// <summary>
        /// maze rows
        /// </summary>
        int mazeRows;

        /// <summary>
        /// maze cols
        /// </summary>
        int mazeCols;

        /// <summary>
        /// maze rows property
        /// </summary>
        public int MazeRows
        {
            get { return Properties.Settings.Default.MazeRows; }
            set { mazeRows = value; }
        }

        /// <summary>
        /// maze cols property
        /// </summary>
        public int MazeCols
        {
            get { return Properties.Settings.Default.MazeCols; }
            set { mazeCols = value; }
        }
    }
}
