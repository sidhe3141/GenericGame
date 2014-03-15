using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sidhe3141utils.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace sidhe3141utils.Xna.UI
{
    /// <summary>
    /// An input text box.
    /// </summary>
    public class UITextBox:UIElement
    {
        #region Fields
        bool active;
        #endregion
        #region Constants
        #endregion
        #region Constructor
        public UITextBox(Rectangle drawRectangle, SpriteFont font, Texture2D sprite)
            : base(drawRectangle, font, drawRectangle.Width - 10, sprite, 3, "")
        {
            active = false;
        }
        #endregion
        #region Methods
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            DrawText(spriteBatch,DrawRectangle.X+5,DrawRectangle.Y+5);
            if (active)
            {
                Frame = 1;
            }
            else
            {
                Frame = 0;
            }
        }

        public override void HandleAction(sidhe3141utils.Xna.Action_and_Input.Action action)
        {
            if (action is ControlClickedAction)
            {
                if (action.Data[0] == this)
                {
                    active = true;
                    Text = "";
                    Frame = 1;
                }
                else
                {
                    active = false;
                    Frame = 0;
                }
            }
            else if (action is KeyTypedAction & active)
            {
                switch ((Keys)action.Data[0])
                {
                    case Keys.Back:
                        //Remove most recent element from text.
                        string data = Text;
                        data = data.Remove(data.Length - 2);
                        Text = data;
                        break;
                    case Keys.LeftShift:
                        break;
                    case Keys.RightShift:
                        break;
                    case Keys.CapsLock:
                        break;
                    case Keys.Enter:
                        active = false;
                        break;
                    default:
                        Text += GameWithUI.translateKey((Keys)action.Data[0]);
                        Frame = 2;
                        break;
                }
            }
        }

        public override void HandleSelection()
        {
            base.HandleSelection();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets and sets whether the text field is monitoring key presses.
        /// </summary>
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }
        #endregion
    }
}
