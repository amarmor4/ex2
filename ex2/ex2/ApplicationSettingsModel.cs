using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex2
{
    /// <summary>
    /// implementation for interface settings model
    /// </summary>
    class ApplicationSettingsModel : ISettingsModel
    {
        /// <summary>
        /// server ip property.
        /// </summary>
        public string ServerIP
        {
            get { return Properties.Settings.Default.ServerIP; }
            set { Properties.Settings.Default.ServerIP = value; }
        }

        /// <summary>
        /// server port property.
        /// </summary>
        public int ServerPort
        {
            get { return Properties.Settings.Default.ServerPort; }
            set { Properties.Settings.Default.ServerPort = value; }
        }

        /// <summary>
        /// maze rows property
        /// </summary>
        public int MazeRows
        {
            get { return Properties.Settings.Default.MazeRows; }
            set { Properties.Settings.Default.MazeRows = value; }
        }

        /// <summary>
        /// maze cols property
        /// </summary>
        public int MazeCols {
            get { return Properties.Settings.Default.MazeCols; }
            set { Properties.Settings.Default.MazeCols = value; }
        }

        /// <summary>
        /// search algorithm property.
        /// </summary>
        public int SearchAlgorithm {
            get { return Properties.Settings.Default.SearchAlgorithm; }
            set { Properties.Settings.Default.SearchAlgorithm = value; }
        }

        /// <summary>
        /// save settings at app.config.
        /// </summary>
        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }
    }
}
