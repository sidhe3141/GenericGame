using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sidhe3141utils.Xna;
using sidhe3141utils.WordWrap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sidhe3141utils.Xna.GameObject;
using sidhe3141utils.Xna.Action_and_Input;

namespace sidhe3141utils.Xna.UI
{
    /// <summary>
    /// A part of a user interface.
    /// </summary>
    public abstract class UIElement:GameObject2D
    {
        #region Fields
        SpriteFont font;
        TextBox text;
        #endregion
        #region Constants
        public const int SELECTED = 0;
        public const int FONTCHAR_M = 13;
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new UI Element
        /// </summary>
        /// <param name="drawRectangle">The draw rectangle of the component's active portion.</param>
        /// <param name="font">The font used to draw text.</param>
        /// <param name="textWidth">The width of the text in pixels.</param>
        /// <param name="sprite">The sprite used for the UI element.</param>
        /// <param name="frames">The number of frames in the UI animation.</param>
        /// <param name="text">The text to display with the UI element.</param>
        public UIElement(Rectangle drawRectangle, SpriteFont font, int textWidth, Texture2D sprite, int frames, string text):
            base(drawRectangle,sprite,frames,1)
        {
            this.font = font;
            this.text = new TextBox(text,textWidth);
        }
        #endregion
        #region Methods

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }
        /// <summary>
        /// Draws the text at specified coordinates.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch used for drawing the text.</param>
        /// <param name="x">The x-coordinate of the text.</param>
        /// <param name="y">The y-coordinate of the text.</param>
        public void DrawText(SpriteBatch spriteBatch, int x, int y)
        {
            spriteBatch.DrawString(font, Text, new Vector2(x, y), Color.Black);
        }
        /// <summary>
        /// User-defined method for handling a click on the draw rectangle.
        /// </summary>
        public virtual void HandleSelection()
        {
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the entire size of the UI element.
        /// </summary>
        public virtual Rectangle EffectRectangle
        {
            get
            {
                return DrawRectangle;
            }
        }
        /// <summary>
        /// Gets and sets the text that is displayed. If set, the textbox used will be completed.
        /// </summary>
        public virtual string Text
        {
            get
            {
                return text.DisplayedContents;
            }
            set
            {
                text.Contents = value;
                text.Complete();
            }
        }
        /// <summary>
        /// Gets the font of the text associated with the object.
        /// </summary>
        public SpriteFont Font
        {
            get
            {
                return font;
            }
        }
        #endregion
        #region Overridden
        public override void MoveTo(Rectangle location)
        {
            base.MoveTo(location);
            Actions.Enqueue(new ControlMovedAction(this));
        }
        #endregion
    }
}
