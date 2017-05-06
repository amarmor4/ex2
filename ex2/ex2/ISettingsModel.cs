using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex2
{
    /// <summary>
    /// interface ettings model.
    /// </summary>
    interface ISettingsModel
    {
        /// <summary>
        /// server ip property.
        /// </summary>
        string ServerIP { get; set; }

        /// <summary>
        /// server port property.
        /// </summary>
        int ServerPort { get; set; }

        /// <summary>
        /// maze rows property
        /// </summary>
        int MazeRows { get; set; }

        /// <summary>
        /// maze cols property
        /// </summary>
        int MazeCols { get; set; }

        /// <summary>
        /// search algorithm property.
        /// </summary>
        int SearchAlgorithm { get; set; }

        /// <summary>
        /// save settings at app.config.
        /// </summary>
        void SaveSettings();
    }
}
