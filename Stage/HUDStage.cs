using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using sidhe3141utils.Xna.Action_and_Input;

namespace sidhe3141utils.Xna.Stage
{
    /// <summary>
    /// An overlay on the game world, used to track data.
    /// Author: James Yakura/sidhe3141
    /// </summary>
    public class HUDStage:Stage
    {
        public HUDStage()
            : base(false)
        {
        }

        /// <summary>
        /// Handles update logic.
        /// </summary>
        /// <param name="gameTime">The elapsed time.</param>
        /// <param name="underlying">The stage about which data is being displayed.</param>
        public virtual void Update(GameTime gameTime, Stage underlying)
        {
            UpdateAllObjects(gameTime);
            CheckRaisedActions();
            SpawnObjects();
            UpdateLogic(gameTime,underlying);
            CheckDeletions();
        }

        /// <summary>
        /// Superceded and deprecated. Put all user-defined update logic in UpdateLogic(GameTime, Stage).
        /// </summary>
        /// <param name="gameTime"></param>
        public override void UpdateLogic(GameTime gameTime)
        {
        }

        /// <summary>
        /// Handles user-defined update logic.
        /// </summary>
        /// <param name="gameTime">The elapsed time.</param>
        /// <param name="underlying">The stage about which data is being displayed.</param>
        public virtual void UpdateLogic(GameTime gameTime, Stage underlying)
        {
        }

        public override void HandleActionLogic(sidhe3141utils.Xna.Action_and_Input.Action action)
        {
        }
    }
}
