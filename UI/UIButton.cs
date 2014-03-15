using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sidhe3141utils.Xna.UI
{
    /// <summary>
    /// A button in a user interface.
    /// </summary>
    public class UIButton:UIElement
    {
        #region Fields
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new button.
        /// </summary>
        /// <param name="drawRectangle">The draw rectangle of the button.</param>
        /// <param name="font">The font used for drawing text.</param>
        /// <param name="sprite">The sprite associated with the button.</param>
        /// <param name="text">The text displayed on the button.</param>
        public UIButton(Rectangle drawRectangle, SpriteFont font, Texture2D sprite, string text)
            : base(drawRectangle, font, drawRectangle.Width - 10, sprite, 2, text)
        {
        }
        #endregion
        #region Methods
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            DrawText(spriteBatch, DrawRectangle.Top + 5, DrawRectangle.Left + 5);
            Frame = 0;
        }

        public override void HandleAction(sidhe3141utils.Xna.Action_and_Input.Action action)
        {
            if (action.Data[0] == this)
            {
                if(action is ControlMousedownAction)
                {
                    Frame = 1;
                }
                else if (action is ControlClickedAction)
                {
                    HandleSelection();
                }
            }
        }

        public override void HandleSelection()
        {
        }
        #endregion
    }
}
