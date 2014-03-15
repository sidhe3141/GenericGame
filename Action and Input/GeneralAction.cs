using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sidhe3141utils.Xna.Action_and_Input
{
    /// <summary>
    /// Handles an action targeted at no particular game entity, such as scrolling, sync calls, or camera rotation.
    /// Author: James Yakura
    /// </summary>
    public class GeneralAction:Action
    {
         bool allLevels;

        /// <summary>
        /// Creates a new Action.
        /// </summary>
        /// <param name="data">Information to be passed with the action.</param>
        /// <param name="universal">True if the event should affect all stages; false if it should only affect the active stage or the HUD.</param>
        public GeneralAction(object[] data, bool universal):base(data)
        {
            allLevels = universal;
        }


        /// <summary>
        /// Gets whether the event affects all stages, or just the active and HUD stages.
        /// </summary>
        public bool AffectsAllStages
        {
            get
            {
                return allLevels;
            }

        }
    }
}
