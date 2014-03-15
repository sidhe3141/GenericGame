using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sidhe3141utils.Xna.Action_and_Input
{
    /// <summary>
    /// Handles events such as user input, calls to sync with server state, and so on.
    /// </summary>
    public class Action
    {
        object[] data;

        /// <summary>
        /// Creates a new Action.
        /// </summary>
        /// <param name="contents">An array of objects passed as data.</param>
        public Action(object[] contents)
        {
            data = contents;
        }
        /// <summary>
        /// Gets the data associated with the action.
        /// </summary>
        public object[] Data
        {
            get
            {
                return data;
            }
        }
    }
}
