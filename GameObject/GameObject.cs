using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sidhe3141utils.Xna.Stage;
using sidhe3141utils.Xna.Action_and_Input;

namespace sidhe3141utils.Xna.GameObject
{
    /// <summary>
    /// An object in an XNA game.
    /// Author: James Yakura/sidhe3141
    /// </summary>
    public abstract class GameObject
    {
        #region Fields
        List<GameObject> spawnList;
        bool deletion;
        Stage.Stage nextStage;
        stageEnd transition;
        int frame;
        Queue<sidhe3141utils.Xna.Action_and_Input.Action> actions;
        Color color;

        //Variables needed for reversion.
        int revertFrame;
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new game object.
        /// </summary>
        public GameObject()
        {
            spawnList = new List<GameObject>();
            deletion = false;
            nextStage = null;
            transition = stageEnd.Stay;
            frame = 0;
            revertFrame = 0;
            actions = new Queue<sidhe3141utils.Xna.Action_and_Input.Action>();
            color = Color.White;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets and sets the list of objects to spawn.
        /// </summary>
        public List<GameObject> SpawnList
        {
            get
            {
                return spawnList;
            }
            set
            {
                spawnList = value;
            }
        }
        /// <summary>
        /// Gets and sets whether to delete the object at the end of the tick. Note: this will not erase other references!
        /// To ensure that the deleted object is subject to garbage collection, remove all other references before end of tick!
        /// </summary>
        public bool Deletion
        {
            get
            {
                return deletion;
            }
            set
            {
                deletion = value;
            }
        }
        /// <summary>
        /// Gets and sets the next stage.
        /// </summary>
        public Stage.Stage NextStage
        {
            get
            {
                return nextStage;
            }
            set
            {
                nextStage = value;
            }
        }
        /// <summary>
        /// Stores what to do at the end of the stage.
        /// </summary>
        public stageEnd Transition
        {
            get
            {
                return transition;
            }
        }
        /// <summary>
        /// Gets and sets the animation frame.
        /// </summary>
        public int Frame
        {
            get
            {
                return frame;
            }
            set
            {
                frame = value;
            }
        }
        /// <summary>
        /// Gets and sets the actions raised by this object.
        /// </summary>
        public Queue<sidhe3141utils.Xna.Action_and_Input.Action> Actions
        {
            get
            {
                return actions;
            }
            set
            {
                actions = value;
            }
        }
        /// <summary>
        /// Gets and sets the color.
        /// </summary>
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Handles the object's update logic.
        /// </summary>
        /// <param name="gameTime">The elapsed game time.</param>
        public abstract void Update(GameTime gameTime);
        /// <summary>
        /// Draws the object at the current frame.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to be used for drawing.</param>
        public abstract void Draw(SpriteBatch spriteBatch);


        /// <summary>
        /// Sets up the program to close the current stage at the end of the tick, returning the game to the previous stage.
        /// </summary>
        public void CloseCurrentStage()
        {
            transition = stageEnd.Close;
        }
        /// <summary>
        /// Sets up the program to open a new stage, placing it on the stack above the current one.
        /// </summary>
        /// <param name="nextStage">The stage to be opened.</param>
        public void OpenStage(Stage.Stage nextStage)
        {
            this.nextStage = nextStage;
            transition = stageEnd.Open;
        }
        /// <summary>
        /// Sets up the program to change to a new stage, replacing the current one on the stack.
        /// </summary>
        /// <param name="nextStage">The stage to be opened.</param>
        public void ChangeStage(Stage.Stage nextStage)
        {
            this.nextStage = nextStage;
            transition = stageEnd.Change;
        }
        /// <summary>
        /// Cancels any transitions in effect.
        /// </summary>
        public void CancelTransition()
        {
            nextStage = null;
            transition = stageEnd.Stay;
        }
        /// <summary>
        /// Saves the object's state for reversion.
        /// </summary>
        public virtual void Snapshot()
        {
            revertFrame = frame;
        }
        /// <summary>
        /// Reverts the object to its saved state.
        /// </summary>
        public virtual void Revert()
        {
            frame = revertFrame;
        }
        ///// <summary>
        ///// Creates an XML tag representing this item.
        ///// </summary>
        //public virtual XmlElement Serialize()
        //{

        //}
        ///// <summary>
        ///// Creates the XML tag associated with this item. Override this in all subclasses.
        ///// </summary>
        ///// <returns>A blank tag of the "GameObject" type</returns>
        //public virtual XmlElement SetUpTag()
        //{
        //    XmlElement result = new XmlElement();
        //    result.
        //}
        /// <summary>
        /// Handles an action passed by the game or another object.
        /// </summary>
        /// <param name="action">The action to be handled.</param>
        public abstract void HandleAction(sidhe3141utils.Xna.Action_and_Input.Action action);
        #endregion
    }
}
