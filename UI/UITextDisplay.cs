using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sidhe3141utils.WordWrap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sidhe3141utils.Xna.GameObject;
using sidhe3141utils.Xna.Action_and_Input;

namespace sidhe3141utils.Xna.UI
{
    public class UITextDisplay:UIElement
    {
        #region Fields
        Color color;
        #endregion
        #region Constructor
        public UITextDisplay(Rectangle drawRectangle, SpriteFont font, Texture2D sprite, Color color)
            : base(drawRectangle, font, (int) font.MeasureString("m").X, sprite, 1, "")
        {
            this.color = color;
        }
        #endregion
        #region Methods
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(Font, Text, new Vector2(DrawRectangle.Left + 5, DrawRectangle.Top + 5), color);
        }

        public override void HandleAction(sidhe3141utils.Xna.Action_and_Input.Action action)
        {
            if (action is TargetedAction & action.Data[0] is string)
            {
                Text = (string)action.Data[0];
            }
        }
        #endregion
        #region Properties
        public Color TextColor
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
    }
}
