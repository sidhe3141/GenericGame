using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sidhe3141utils.Xna;
using sidhe3141utils.Xna.Action_and_Input;
using Microsoft.Xna.Framework;

namespace sidhe3141utils.Xna.UI
{
    /// <summary>
    /// A stage that supports user-interface elements.
    /// </summary>
    public abstract class StageWithUI:Stage.Stage
    {
        #region Constructor
        public StageWithUI(bool hudstage)
            : base(hudstage)
        {
        }

        public StageWithUI()
            : base(false)
        {
        }
        #endregion

        #region Methods

        public override void HandleAction(sidhe3141utils.Xna.Action_and_Input.Action action)
        {

            if (action is ObjectAddAction)
            {
                if (action.Data[0] is UIElement & action.Data[1] == this)
                {
                    AddInput((UIElement)action.Data[0]);
                }
            }
            else if (action is ObjectRemoveAction)
            {
                    if (action.Data[0] is UIElement & action.Data[1] == this)
                    {
                        List<KeyValuePair<Input[], sidhe3141utils.Xna.Action_and_Input.Action>> removals = new List<KeyValuePair<Input[], sidhe3141utils.Xna.Action_and_Input.Action>>();
                        //Look for controls to remove.
                        foreach (KeyValuePair<Input[], sidhe3141utils.Xna.Action_and_Input.Action> x in Controls)
                        {
                            if (x.Value.Data[0] == action.Data[0])
                            {
                                removals.Add(x);
                            }
                        }
                        //Remove all controls for which a remove is indicated.
                        foreach (KeyValuePair<Input[], sidhe3141utils.Xna.Action_and_Input.Action> x in removals)
                        {
                            Controls.Remove(x.Key);
                        }
                    }
                    
                }
            else if (action is ControlMovedAction)
            {
                foreach (KeyValuePair<Input[], sidhe3141utils.Xna.Action_and_Input.Action> x in Controls)
                {
                    if (x.Value.Data[0] == action.Data[0])
                    {
                        foreach(Input y in x.Key)
                        {
                            if (y.Bounds != null)
                            {
                                y.Bounds = ((UIElement)action.Data[0]).EffectRectangle;
                            }
                        }
                    }
                }
            }
            base.HandleAction(action);
        }
        /// <summary>
        /// Adds a new input associated with a UI element.
        /// </summary>
        /// <param name="element">The UI element to add the input for.</param>
        public void AddInput(UIElement element)
        {
            //Create an input[] containing the following inputs: Stage=x, mouse in UIElement target rectangle, left mouse clicked.
            Input[] inputs = new Input[2];
            inputs[0] = new Input(EventType.Down, MouseButtons.left);
            inputs[1] = new Input(EventType.Down, Sticks.mouse, element.DrawRectangle, PlayerIndex.One);
            //Create an action.
            sidhe3141utils.Xna.Action_and_Input.Action action = new ControlMousedownAction(element);
            //Add the action to the bindings and the controls.
            Controls.Add(inputs, action);
            Input[] inputs2 = new Input[2];
            action = new ControlClickedAction(element);
            inputs2[0] = new Input(EventType.Typed, MouseButtons.left);
            inputs2[1] = inputs[1];
            Controls.Add(inputs2,action);
        }
        #endregion
    }
}
