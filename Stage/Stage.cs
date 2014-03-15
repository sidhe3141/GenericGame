using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sidhe3141utils.Xna.Action_and_Input;

namespace sidhe3141utils.Xna.Stage
{
    /// <summary>
    /// Stores what to do at the end of the current tick.
    /// </summary>
    public enum stageEnd
    {
        /// <summary>
        /// Stay on the current stage.
        /// </summary>
        Stay,
        /// <summary>
        /// Open a new stage, placing it above this one on the stack.
        /// </summary>
        Open,
        /// <summary>
        /// Change stages, replacing this stage with another.
        /// </summary>
        Change,
        /// <summary>
        /// Close this stage, returning to the stage below it on the stack.
        /// </summary>
        Close
    }

    /// <summary>
    /// A level, menu, or the like in an XNA game.
    /// Author: James Yakura/sidhe3141
    /// </summary>
    public abstract class Stage
    {
        #region Fields
        List<GameObject.GameObject> objects;
        List<GameObject.GameObject> spawnObjects;
        stageEnd transition;
        Stage nextStage;
        bool displayHUD;
        bool unloadWhenFinished;
        //Reversion variables
        List<GameObject.GameObject> revertObjects;
        Queue<sidhe3141utils.Xna.Action_and_Input.Action> actions;
        Dictionary<Input[],sidhe3141utils.Xna.Action_and_Input.Action> controls;
        bool loaded;
        #endregion

        #region Constants
        public const int DEFAULT_WIDTH = 200;
        public const int DEFAULT_HEIGHT = 200;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new stage.
        /// </summary>
        /// <param name="dispHud">Whether the HUD is visible on this stage.</param>
        /// <param name="camera">The camera used for dispalying the stage.</param>
        public Stage(bool dispHud)
        {
            objects = new List<GameObject.GameObject>();
            spawnObjects = new List<GameObject.GameObject>();
            transition = stageEnd.Stay;
            nextStage = null;
            revertObjects = new List<GameObject.GameObject>();
            displayHUD = dispHud;
            actions = new Queue<sidhe3141utils.Xna.Action_and_Input.Action>();
            controls = new Dictionary<Input[], sidhe3141utils.Xna.Action_and_Input.Action>();
            unloadWhenFinished = false;
        }

        public Stage()
            : this(false)
        {
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets whether to move to the next stage at the end of the current tick.
        /// </summary>
        public stageEnd Transition
        {
            get
            {
                return transition;
            }
        }
        /// <summary>
        /// Gets the next stage to move to.
        /// </summary>
        public Stage NextStage
        {
            get
            {
                return nextStage;
            }
        }
        /// <summary>
        /// Gets whether to display the HUD.
        /// </summary>
        public bool DisplayHUD
        {
            get
            {
                return displayHUD;
            }
            set
            {
                displayHUD = value;
            }
        }
        /// <summary>
        /// Gets and sets the actions raised this tick.
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
        /// Gets and sets the list of game objects on the stage.
        /// </summary>
        public List<GameObject.GameObject> Objects
        {
            get
            {
                return objects;
            }
            set
            {
                objects = value;
            }
        }
        /// <summary>
        /// Gets and sets the list of objects to spawn.
        /// </summary>
        public List<GameObject.GameObject> SpawnList
        {
            get
            {
                return spawnObjects;
            }
            set
            {
                spawnObjects = value;
            }
        }
        /// <summary>
        /// Gets the controls of things on this stage.
        /// </summary>
        public virtual Dictionary<Input[], sidhe3141utils.Xna.Action_and_Input.Action> Controls
        {
            get
            {
                return controls;
            }
        }
        /// <summary>
        /// Gets whether the level has been loaded.
        /// </summary>
        public bool Loaded
        {
            get
            {
                return loaded;
            }
        }
        /// <summary>
        /// Gets whether to unload the level when finished with it.
        /// </summary>
        public bool UnloadWhenFinished
        {
            get { return unloadWhenFinished; }
            set { unloadWhenFinished = value; }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Updates the stage.
        /// </summary>
        /// <param name="gameTime">The elapsed time.</param>
        public void Update(GameTime gameTime)
        {
            //Update all objects on the stage.
            UpdateAllObjects(gameTime);
            //Check for a stage transition and clear all transition flags.
            foreach (GameObject.GameObject x in objects)
            {
                if (x.Transition != stageEnd.Stay)
                {
                    transition = x.Transition;
                    nextStage = x.NextStage;
                    x.CancelTransition();
                }
            }
            //Handle actions and spawning.
            CheckRaisedActions();
            //Handle user-defined update logic.
            UpdateLogic(gameTime);
            //Spawn objects that have a spawn called for.
            SpawnObjects();
            //Delete objects that have a delete called for.
            CheckDeletions();
        }
        /// <summary>
        /// Saves the current stage state for reversion.
        /// </summary>
        public virtual void Snapshot()
        {
            revertObjects = new List<GameObject.GameObject>();
            foreach (GameObject.GameObject x in objects)
            {
                x.Snapshot();
                revertObjects.Add(x);
            }
        }
        /// <summary>
        /// Reverts the current stage to its saved state.
        /// </summary>
        public virtual void Revert()
        {
            objects = new List<GameObject.GameObject>();
            foreach (GameObject.GameObject x in revertObjects)
            {
                objects.Add(x);
                x.Revert();
            }
        }
        /// <summary>
        /// Handles update logic.
        /// </summary>
        /// <param name="gameTime">The elapsed time.</param>
        public virtual void UpdateLogic(GameTime gameTime)
        {
        }
        /// <summary>
        /// Clears out all transitions.
        /// </summary>
        public void clearTransition()
        {
            transition = stageEnd.Stay;
            nextStage = null;
        }
        /// <summary>
        /// Draws the stage.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch to be used for drawing.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                objects[i].Draw(spriteBatch);
            }
        }
        /// <summary>
        /// Causes all objects in the stage to handle an action.
        /// </summary>
        /// <param name="action">The action to be handled.</param>
        public virtual void HandleAction(sidhe3141utils.Xna.Action_and_Input.Action action)
        {

            foreach (GameObject.GameObject x in objects)
            {
                x.HandleAction(action);
            }
            HandleActionLogic(action);
        }
        /// <summary>
        /// Handles stagewide events (revert, rule change, and so on)
        /// </summary>
        /// <param name="action">The action to be passed.</param>
        public abstract void HandleActionLogic(sidhe3141utils.Xna.Action_and_Input.Action action);
        /// <summary>
        /// Checks for actions raised by objects in the stage.
        /// </summary>
        public void CheckRaisedActions()
        {
            foreach (GameObject.GameObject x in objects)
            {
                foreach (sidhe3141utils.Xna.Action_and_Input.Action y in x.Actions)
                {
                    actions.Enqueue(y);
                }
                x.Actions = new Queue<sidhe3141utils.Xna.Action_and_Input.Action>();
                foreach (GameObject.GameObject y in x.SpawnList)
                {
                    spawnObjects.Add(y);
                }
                x.SpawnList = new List<GameObject.GameObject>();
            }
        }
        /// <summary>
        /// Checks for objects that should be deleted.
        /// </summary>
        public void CheckDeletions()
        {
            for (int i = objects.Count-1; i >= 0; i--)
            {
                if (objects[i].Deletion)
                {
                    Actions.Enqueue(new ObjectRemoveAction(objects[i], this));
                    objects.Remove(objects[i]);
                }
            }
        }
        /// <summary>
        /// Updates all objects in the stage.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void UpdateAllObjects(GameTime gameTime)
        {
            foreach (GameObject.GameObject x in objects)
            {
                x.Update(gameTime);
            }
        }
        /// <summary>
        /// Spawns all objects in the spawn list and clears the spawn list.
        /// </summary>
        public void SpawnObjects()
        {
            foreach (GameObject.GameObject x in spawnObjects)
            {
                objects.Add(x);
                Actions.Enqueue(new ObjectAddAction( x, this ));
            }
            spawnObjects = new List<GameObject.GameObject>();
        }
        /// <summary>
        /// Loads the level.
        /// </summary>
        /// <param name="content">The content manager to use for loading the level.</param>
        public virtual void Load(ContentManager content)
        {
            loaded = true;
        }
        /// <summary>
        /// Releases the assets used in keeping the level loaded.
        /// </summary>
        public virtual void Unload()
        {
            loaded = false;
        }
        #endregion
    }
}
