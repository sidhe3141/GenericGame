using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sidhe3141utils.Xna;

namespace sidhe3141utils.Xna.UI
{
    /// <summary>
    /// A check box.
    /// </summary>
    public class UICheckBox:UIElement
    {
        #region Fields
        bool active;
        bool locked;
        int textfieldWidth;
        int textfieldHeight;
        #endregion
        #region Constants
        public const int INACTIVE_UNLOCKED_FRAME = 0;
        public const int ACTIVE_UNLOCKED_FRAME = 1;
        public const int INACTIVE_LOCKED_FRAME = 2;
        public const int ACTIVE_LOCKED_FRAME = 3;
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new check box.
        /// </summary>
        /// <param name="drawRectangle">The draw rectangle of the check box.</param>
        /// <param name="font">The font to be used for drawing text.</param>
        /// <param name="effectiveWidth">The width of the checkbox's text.</param>
        /// <param name="sprite">The sprite of the checkbox.</param>
        /// <param name="text">The text to be used in the text box.</param>
        public UICheckBox(Rectangle drawRectangle, SpriteFont font, int effectiveWidth, Texture2D sprite, string text)
            : base(drawRectangle, font, effectiveWidth-drawRectangle.Width, sprite, 4, text)
        {
            active = false;
            locked = false;
            textfieldWidth = effectiveWidth - drawRectangle.Width;
            textfieldHeight = font.LineSpacing;
            foreach (char x in this.Text)
            {
                if (x == '\n')
                {
                    textfieldHeight += font.LineSpacing;
                }
            }
        }
        #endregion
        #region Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                if (locked)
                {
                    Frame = ACTIVE_LOCKED_FRAME;
                }
                else
                {
                    Frame = ACTIVE_UNLOCKED_FRAME;
                }
            }
            else
            {
                if (locked)
                {
                    Frame = INACTIVE_LOCKED_FRAME;
                }
                else
                {
                    Frame = INACTIVE_UNLOCKED_FRAME;
                }
            }
            base.Draw(spriteBatch);
        }

        public override void HandleAction(sidhe3141utils.Xna.Action_and_Input.Action action)
        {
            if (action is ControlClickedAction & action.Data[0]==this & !locked)
            {
                active = !active;
            }
        }

        #endregion
        #region Properties
        /// <summary>
        /// Gets and sets whether the checkbox is checked.
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
        /// <summary>
        /// Gets and sets whether the checkbox is editable.
        /// </summary>
        public bool Locked
        {
            get
            {
                return locked;
            }
            set
            {
                locked = value;
            }
        }

        public override Rectangle EffectRectangle
        {
            get
            {
                Rectangle value = new Rectangle(DrawRectangle.X, DrawRectangle.Y, textfieldWidth + DrawRectangle.Width, textfieldHeight);
                return value;
            }
        }
        #endregion
    }
}
