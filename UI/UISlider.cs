using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using sidhe3141utils.Xna;

namespace sidhe3141utils.Xna.UI
{
    /// <summary>
    /// A slider input.
    /// </summary>
    public class UISlider: UIElement
    {
        #region Fields
        bool horizontal;
        bool vertical;
        Texture2D sliderSprite;
        Point position;
        Texture2D sliderDrawRectangle;
        #endregion
        #region Constants
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new slider.
        /// </summary>
        /// <param name="drawRectangle">The draw rectangle of the slider.</param>
        /// <param name="font">A spritefont. Use any spritefont, but do not leave null.</param>
        /// <param name="sprite">The sprite of the slider background.</param>
        /// <param name="pointerSprite">The sprite of the slider pointer. Create at 1:1 scale.</param>
        /// <param name="horizActive">True if the slider is active horizontally, false otherwise.</param>
        /// <param name="vertActive">True if the slider is active vertically, false otherwise.</param>
        public UISlider(Rectangle drawRectangle, SpriteFont font, Texture2D sprite, Texture2D pointerSprite, bool horizActive, bool vertActive)
            : base(drawRectangle, font, 100, sprite, 1, "")
        {
            sliderSprite = sprite;
            horizontal = horizActive;
            vertical = vertActive;
            position = new Point(drawRectangle.Center.X,drawRectangle.Center.Y);
            sliderDrawRectangle = pointerSprite;
        }
        #endregion
        #region Methods
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(sliderDrawRectangle, 
                new Rectangle(
                position.X - sliderDrawRectangle.Bounds.Width / 2, 
                position.Y - sliderDrawRectangle.Bounds.Height / 2,
                sliderDrawRectangle.Bounds.Height, 
                sliderDrawRectangle.Bounds.Width), 
                Color.White);
        }

        public override void HandleAction(sidhe3141utils.Xna.Action_and_Input.Action action)
        {
            if (action is ControlMousedownAction & action.Data[0]==this)
            {
                if (horizontal) { position.X = Mouse.GetState().X; }
                if (vertical) { position.Y = Mouse.GetState().Y; }
            }
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets and sets the x-percentage of the slider.
        /// </summary>
        public float X
        {
            get
            {
                return (float)(position.X - DrawRectangle.X) / (float)DrawRectangle.Width;
            }
            set
            {
                position.X = (int)(value * DrawRectangle.Width + DrawRectangle.X);
            }
        }

        /// <summary>
        /// Gets and sets the y-percentage of the slider.
        /// </summary>
        public float Y
        {
            get
            {
                return (float)(position.Y - DrawRectangle.Y) / (float)DrawRectangle.Height;
            }
            set
            {
                position.Y = (int)(value * DrawRectangle.Height + DrawRectangle.Y);
            }
        }
        /// <summary>
        /// Gets and sets whether the slider can move on the horizontal axis.
        /// </summary>
        public bool Horizontal
        {
            get
            {
                return horizontal;
            }
            set
            {
                horizontal = value;
            }
        }
        /// <summary>
        /// Gets and sets whether the slider can move on the vertical axis.
        /// </summary>
        public bool Vertical
        {
            get
            {
                return vertical;
            }
            set
            {
                vertical = value;
            }
        }
        #endregion
    }
}
