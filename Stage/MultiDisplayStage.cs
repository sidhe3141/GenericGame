using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using sidhe3141utils.Xna;
using sidhe3141utils.Xna.Action_and_Input;
using sidhe3141utils.Xna.Game;
using sidhe3141utils.Xna.GameObject;
using sidhe3141utils.Xna.Stage;
using sidhe3141utils.Xna.UI;

namespace sidhe3141utils.Xna.Stage
{
    /// <summary>
    /// A stage capable of displaying multiple "substages", such as a nested menu.
    /// </summary>
    public abstract class MultiDisplayStage:Stage
    {
        #region Fields
        Stack<Stage> substages;
        ContentManager content;
        #endregion
        #region Constructor
        public MultiDisplayStage():base()
        {
            substages = new Stack<Stage>();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the substages.
        /// </summary>
        public Stack<Stage> SubStages
        {
            get
            {
                return substages;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Opens a new stage.
        /// </summary>
        /// <param name="substage">The substage to open.</param>
        public void OpenSubstage(Stage substage)
        {
            if(!substage.Loaded)
            {
                substage.Load(content);
            }
            substages.Push(substage);
        }
        /// <summary>
        /// Closes the topmost substage.
        /// </summary>
        public void CloseSubstage()
        {
            if (substages.Peek().UnloadWhenFinished)
            {
                substages.Pop().Unload();
            }
            else
            {
                substages.Pop();
            }
        }
        /// <summary>
        /// Changes the topmost substage.
        /// </summary>
        /// <param name="substage">The substage to change to.</param>
        public void ChangeSubstage(Stage substage)
        {
            CloseSubstage();
            OpenSubstage(substage);
        }
        #endregion
        #region Overridden
        public override void Load(ContentManager content)
        {
            this.content = content;
            base.Load(content);
        }

        public override Dictionary<Input[], sidhe3141utils.Xna.Action_and_Input.Action> Controls
        {
            get
            {
                Dictionary<Input[], sidhe3141utils.Xna.Action_and_Input.Action> value = new Dictionary<Input[], sidhe3141utils.Xna.Action_and_Input.Action>();
                foreach (KeyValuePair<Input[], sidhe3141utils.Xna.Action_and_Input.Action> x in base.Controls)
                {
                    value.Add(x.Key,x.Value);
                }
                foreach (KeyValuePair<Input[], sidhe3141utils.Xna.Action_and_Input.Action> x in substages.Peek().Controls)
                {
                    value.Add(x.Key, x.Value);
                }
                return value;
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Stack<Stage> drawStack = new Stack<Stage>();
            foreach(Stage x in substages)
            {
                drawStack.Push(x);
            }
            while (drawStack.Count > 0)
            {
                drawStack.Pop().Draw(spriteBatch);
            }


        }

        public override void Unload()
        {
            content = null;
            substages = new Stack<Stage>();
            base.Unload();
        }

        public override void HandleAction(sidhe3141utils.Xna.Action_and_Input.Action action)
        {

            if (action is GeneralAction)
            {
                if (((GeneralAction)action).AffectsAllStages)
                {
                    foreach (Stage x in substages)
                    {
                        x.HandleAction(action);
                    }
                }
                else
                {
                    substages.Peek().HandleAction(action);
                }
            }
            base.HandleAction(action);
        }

        public override void  UpdateAllObjects(Microsoft.Xna.Framework.GameTime gameTime)
        {
            
            substages.Peek().Update(gameTime);
            while (substages.Peek().Actions.Count > 0)
            {
                Actions.Enqueue(substages.Peek().Actions.Dequeue());
            }
            switch (substages.Peek().Transition)
            {
                case stageEnd.Stay:
                    break;
                case stageEnd.Open:
                    OpenSubstage(substages.Peek().NextStage);
                    break;
                case stageEnd.Change:
                    ChangeSubstage(substages.Peek().NextStage);
                    break;
                case stageEnd.Close:
                    CloseSubstage();
                    break;
            }
            substages.Peek().clearTransition();
            base.UpdateAllObjects(gameTime);
        }
        #endregion
    }
}
