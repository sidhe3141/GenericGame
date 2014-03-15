using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sidhe3141utils.Xna.Action_and_Input
{
    /// <summary>
    /// Handles actions that affect a specific game object, such as interactions, server-state updates, 
    /// </summary>
    public class TargetedAction:Action
    {
        GameObject.GameObject target;
        /// <summary>
        /// Creates a new targeted action.
        /// </summary>
        /// <param name="data">Information associated with the action.</param>
        /// <param name="target">The game object affected by the action.</param>
        public TargetedAction(object[] data, GameObject.GameObject target)
            : base(data)
        {
            this.target = target;
        }

        /// <summary>
        /// Raises the event for the targeted object to handle.
        /// </summary>
        public void Raise()
        {
            target.HandleAction(this);
        }

    }
}
