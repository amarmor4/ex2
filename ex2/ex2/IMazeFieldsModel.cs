using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex2
{
    /// <summary>
    /// interface maze fields model
    /// </summary>
    interface IMazeFieldsModel
    {
        /// <summary>
        /// maze rows property
        /// </summary>
        int MazeRows { get; set; }

        /// <summary>
        /// maze cols property
        /// </summary>
        int MazeCols { get; set; }
    }
}
