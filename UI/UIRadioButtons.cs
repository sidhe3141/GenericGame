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
    /// A set of radio buttons.
    /// </summary>
    public class UIRadioButtons:UIElement
    {
        #region Fields
        UICheckBox[] buttons;
        int selectedButton;
        #endregion
        #region Constants
        const int NO_SELECTION = -1;
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new set of radio buttons. Do not add this to the stage; it will add itself.
        /// </summary>
        /// <param name="drawRectangle">The draw rectangle of the radio buttons.</param>
        /// <param name="font">The font used to draw the button labels.</param>
        /// <param name="sprite">The background sprite of the buttons.</param>
        /// <param name="buttonLabels">The labels on the buttons.</param>
        /// <param name="buttonSprite">The sprite of a button. Must use the same series of icons as a checkbox.</param>
        /// <param name="size">The size of each radio button in pixels.</param>
        public UIRadioButtons(Rectangle drawRectangle, SpriteFont font, Texture2D sprite, string[] buttonLabels, Texture2D buttonSprite, int size)
            : base(drawRectangle, font, 0, sprite, 1, "")
        {
            int ypos = drawRectangle.Y;
            buttons = new UICheckBox[buttonLabels.Length];
            for(int i=0;i<buttonLabels.Length;i++)
            {
                buttons[i] = new UICheckBox(
                    new Rectangle(drawRectangle.X, ypos, size, size),
                    font, 
                    drawRectangle.Width-10, 
                    buttonSprite, 
                    buttonLabels[i]
                    );
                SpawnList.Add(buttons[i]);
                ypos += buttons[i].EffectRectangle.Height;
            }
            drawRectangle.Height = ypos - drawRectangle.Y;
            selectedButton = -1;
        }
        #endregion
        #region Methods
        public override void HandleAction(sidhe3141utils.Xna.Action_and_Input.Action action)
        {
            if (action is ControlClickedAction)
            {
                bool containsButton=false;
                foreach (UICheckBox x in buttons)
                {
                    if (x == action.Data[0])
                    {
                        containsButton = true;
                    }
                }
                if (!containsButton)
                {
                    return;
                }
                selectedButton = -1;
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (buttons[i]==action.Data[0])
                    {
                        selectedButton = i;
                        break;
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (UICheckBox x in buttons)
            {
                x.Active = false;
            }
            if (selectedButton > -1)
            {
                buttons[selectedButton].Active = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            DrawText(spriteBatch, DrawRectangle.Right + 5, DrawRectangle.Y);
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the buttons.
        /// </summary>
        public UICheckBox[] Buttons
        {
            get
            {
                return buttons;
            }
        }
        /// <summary>
        /// Gets and sets the selected button.
        /// </summary>
        public int SelectedButton
        {
            get
            {
                return selectedButton;
            }
            set
            {
                selectedButton = value;
            }
        }
        #endregion
    }
}
