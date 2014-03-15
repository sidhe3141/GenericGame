using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using sidhe3141utils;
using sidhe3141utils.Xna;
using sidhe3141utils.Xna.Action_and_Input;
using sidhe3141utils.Xna.Stage;

namespace sidhe3141utils.Xna.Game
{
    /// <summary>
    /// An XNA game.
    /// Based on the "Game1" template provided with the XNA framework.
    /// Modifier: James Yakura/sidhe3141
    /// </summary>
    public abstract class Game : Microsoft.Xna.Framework.Game
    {
        #region Fields
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Stage.Stage> stages;
        Stack<Stage.Stage> activeStages;
        HUDStage hud;
        Queue<sidhe3141utils.Xna.Action_and_Input.Action> handledActions;
        Dictionary<Input[],sidhe3141utils.Xna.Action_and_Input.Action> bindings;
        Color color;
        #endregion
        #region Constants
        public const int OBJECT_ADD = 0;
        public const int OBJECT_REMOVE = 1;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets and sets the list of stages in the entire game. If anything in the game needs to refer to any stage, that stage must
        /// be passed as a parameter.
        /// </summary>
        public List<Stage.Stage> Stages
        {
            get
            {
                return stages;
            }
            set
            {
                stages = value;
            }
        }
        /// <summary>
        /// Gets and sets the stack containing active stages.
        /// </summary>
        public Stack<Stage.Stage> ActiveStages
        {
            get
            {
                return activeStages;
            }
            set
            {
                activeStages = value;
            }
        }
        /// <summary>
        /// Gets and sets the heads-up display.
        /// </summary>
        public HUDStage HUD
        {
            get
            {
                return hud;
            }
            set
            {
                hud = value;
            }
        }
        /// <summary>
        /// Gets and sets the queue of actions to be performed next tick.
        /// </summary>
        public Queue<sidhe3141utils.Xna.Action_and_Input.Action> Actions
        {
            get
            {
                return handledActions;
            }
            set
            {
                handledActions = value;
            }
        }
        /// <summary>
        /// Gets and sets input bindings.
        /// </summary>
        public Dictionary<Input[], sidhe3141utils.Xna.Action_and_Input.Action> Bindings
        {
            get
            {
                return bindings;
            }
            set
            {
                bindings = value;
            }
        }
        /// <summary>
        /// Gets the game window boundaries.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return Window.ClientBounds;
            }
        }
        /// <summary>
        /// Gets the list of all controls.
        /// </summary>
        public Dictionary<Input[], sidhe3141utils.Xna.Action_and_Input.Action> AllBindings
        {
            get
            {
                Dictionary<Input[], sidhe3141utils.Xna.Action_and_Input.Action> tempBindings = new Dictionary<Input[], sidhe3141utils.Xna.Action_and_Input.Action>();
                foreach (KeyValuePair<Input[], sidhe3141utils.Xna.Action_and_Input.Action> x in bindings)
                {
                    tempBindings.Add(x.Key, x.Value);
                }
                foreach (KeyValuePair<Input[], sidhe3141utils.Xna.Action_and_Input.Action> x in activeStages.Peek().Controls)
                {
                    tempBindings.Add(x.Key, x.Value);
                }
                return tempBindings;
            }
        }
        #endregion
        #region Constructor
        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            stages = new List<Stage.Stage>();
            activeStages = new Stack<Stage.Stage>();
            handledActions = new Queue<sidhe3141utils.Xna.Action_and_Input.Action>();
            color = Color.CornflowerBlue;
        }
        #endregion
        #region Public Methods
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            bindings = new Dictionary<Input[], sidhe3141utils.Xna.Action_and_Input.Action>();
            InitializeLogic();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            hud = new HUDStage();
            ContentLoadLogic(Content);
            LoadLevels();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            ContentUnloadLogic();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            //Fire events related to inputs.
                GrabEvents();
            foreach (KeyValuePair<Input[],sidhe3141utils.Xna.Action_and_Input.Action> x in AllBindings)
            {
                bool execute = true;
                foreach (Input y in x.Key)
                {
                    if (!y.PostProcessValue)
                    {
                        execute = false;
                    }
                }
                if (execute)
                {
                    handledActions.Enqueue(x.Value);
                }
            }
            //Handle user-defined update logic.
            UpdateLogic();
            //Update the active stage.
            activeStages.Peek().Update(gameTime);
            //Get actions from the active stage.
            foreach (sidhe3141utils.Xna.Action_and_Input.Action x in activeStages.Peek().Actions)
            {
                handledActions.Enqueue(x);
            }
            activeStages.Peek().Actions = new Queue<sidhe3141utils.Xna.Action_and_Input.Action>();
            //Execute actions.
            while (handledActions.Count>0)
            {
                sidhe3141utils.Xna.Action_and_Input.Action y = handledActions.Dequeue();
                if (y is GeneralAction)
                {
                    GeneralAction x = (GeneralAction)y;
                    HandleEvents(x);
                    if (x.AffectsAllStages)
                    {
                        foreach (Stage.Stage z in stages)
                        {
                            z.HandleAction(x);
                        }
                    }
                    else
                    {
                        activeStages.Peek().HandleAction(x);
                        hud.HandleAction(x);
                    }
                }
                else
                {
                    ((TargetedAction)y).Raise();
                }
            }
            //Handle stage transitions.
            switch (activeStages.Peek().Transition)
            {
                case stageEnd.Change:
                    Stage.Stage next = activeStages.Peek().NextStage;
                    if (!next.Loaded)
                    {
                        next.Load(Content);
                    }
                    activeStages.Peek().clearTransition();
                    if (activeStages.Peek().UnloadWhenFinished)
                    {
                        activeStages.Peek().Unload();
                    }
                    activeStages.Pop();
                    activeStages.Push(next);
                    break;
                case stageEnd.Close:
                    activeStages.Peek().clearTransition();
                    if (activeStages.Peek().UnloadWhenFinished)
                    {
                        activeStages.Peek().Unload();
                    }
                    activeStages.Pop();
                    break;
                case stageEnd.Open:
                    Stage.Stage nexts = activeStages.Peek().NextStage;
                    activeStages.Peek().clearTransition();
                    nexts.Load(Content);
                    activeStages.Push(nexts);
                    break;
            }
            //Update the HUD.
            hud.Update(gameTime,activeStages.Peek());


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(color);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            activeStages.Peek().Draw(spriteBatch);
            if (activeStages.Peek().DisplayHUD)
            {
                hud.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Handles level loading. Put all levels in the game into the "Stages" list, and the opening screen into the "ActiveStages" stack.
        /// </summary>
        public virtual void LoadLevels()
        {
        }
        /// <summary>
        /// Handles user-defined update logic.
        /// </summary>
        public virtual void UpdateLogic()
        {
        }
        /// <summary>
        /// Handles initialization code.
        /// </summary>
        public virtual void InitializeLogic()
        {
        }
        /// <summary>
        /// Handles content loading.
        /// </summary>
        public virtual void ContentLoadLogic(ContentManager content)
        {
        }
        /// <summary>
        /// Handles content unloading.
        /// </summary>
        public virtual void ContentUnloadLogic()
        {
        }
        /// <summary>
        /// Handles logic for global events (disconnect, save game, and so forth).
        /// </summary>
        /// <param name="action">The event to he handled.</param>
        public virtual void HandleEvents(sidhe3141utils.Xna.Action_and_Input.Action action)
        {
        }
        /// <summary>
        /// Grabs events related to global game state or non-device input (such as web commands or timers).
        /// </summary>
        public virtual void GrabEvents()
        {

        }
        #endregion
    }
}
