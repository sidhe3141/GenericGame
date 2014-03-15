using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sidhe3141utils.Xna;//My XNA tools.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using sidhe3141utils.Xna.Action_and_Input;

namespace sidhe3141utils.Xna.UI
{
    public abstract class GameWithUI:sidhe3141utils.Xna.Game.Game
    {
        #region Fields
        static bool capsLock = false;
        static bool shifted = false;
        #endregion
        #region Constants
        /*
         * Deprecated because it leads to bad coding practice.
        public const int CONTROL_MOUSEDOWN = 2;
        public const int CONTROL_CLICKED = 3;
        public const int KEY_TYPED=4;
        public const int CONTROL_MOVED = 5;
         * */
        #endregion
        #region Methods

        protected override void LoadContent()
        {
            base.LoadContent();
            //Add all valid key presses as inputs.
            Keys[] validInputs = new Keys[]
                { Keys.A,Keys.B,Keys.C,Keys.CapsLock,Keys.D,Keys.D0,Keys.D1,Keys.D2,Keys.D3,Keys.D4,Keys.D5,Keys.D6,Keys.D7,Keys.D8,Keys.D9, 
                    Keys.Decimal,Keys.Divide,Keys.E,Keys.Enter,Keys.F,Keys.G,Keys.H,Keys.I,Keys.J,Keys.K,Keys.L,Keys.LeftShift,Keys.M,Keys.Multiply,
                    Keys.N,Keys.NumPad0,Keys.NumPad1,Keys.NumPad2,Keys.NumPad3,Keys.NumPad4,Keys.NumPad5,Keys.NumPad6,Keys.NumPad7,Keys.NumPad8,
                    Keys.NumPad9,Keys.O,Keys.OemBackslash,Keys.OemCloseBrackets,Keys.OemComma,Keys.OemMinus,Keys.OemOpenBrackets,Keys.OemPeriod,
                    Keys.OemPlus,Keys.OemQuestion,Keys.OemQuotes,Keys.OemSemicolon,Keys.OemTilde,Keys.P,Keys.Q,Keys.R,Keys.RightShift,Keys.S,
                    Keys.Space,Keys.Subtract,Keys.T,Keys.U,Keys.V,Keys.W,Keys.X,Keys.Y,Keys.Z,Keys.Back
                };
            foreach (Keys y in validInputs)
            {
                Input thisKeyInput;
                if (y == Keys.LeftShift | y == Keys.RightShift)
                {
                    thisKeyInput = new Input(EventType.Down, y);
                }
                else
                {
                    thisKeyInput = new Input(EventType.Typed, y);
                }
                Bindings.Add(new Input[] { thisKeyInput }, new KeyTypedAction(y));
            }
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            shifted = Shifted;
        }

        public override void HandleEvents(sidhe3141utils.Xna.Action_and_Input.Action action)
        {
            base.HandleEvents(action);
            if (action is KeyTypedAction & action.Data[0]==(object)Keys.CapsLock)
            {
                capsLock = !capsLock;
            }
            else if (action is ObjectRemoveAction)
            {
            }
        }

        public override void ContentLoadLogic(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.ContentLoadLogic(content);
        }

        public override void ContentUnloadLogic()
        {
            base.ContentUnloadLogic();
        }

        public override void GrabEvents()
        {
            base.GrabEvents();
        }

        public override void InitializeLogic()
        {
            base.InitializeLogic();
        }

        public override void LoadLevels()
        {
            base.LoadLevels();
        }
        /// <summary>
        /// Translates a keypress into the approptiate character.
        /// </summary>
        /// <param name="key">The key pressed.</param>
        /// <returns>The character associated with that key.</returns>
        public static char translateKey(Keys key)
        {
            char result;
            switch (key)
            {
                #region A-E
                case Keys.A:
                    result = 'a';
                    break;
                case Keys.B:
                    result = 'b';
                    break;
                case Keys.C:
                    result = 'c';
                    break;
                case Keys.D:
                    result = 'd';
                    break;
                case Keys.E:
                    result = 'e';
                #endregion
                    #region F-J
                    break;
                case Keys.F:
                    result = 'f';
                    break;
                case Keys.G:
                    result = 'g';
                    break;
                case Keys.H:
                    result = 'h';
                    break;
                case Keys.I:
                    result = 'i';
                    break;
                case Keys.J:
                    result = 'j';
                    break;
                    #endregion
                #region K-O
                case Keys.K:
                    result = 'k';
                    break;
                case Keys.L:
                    result = 'l';
                    break;
                case Keys.M:
                    result = 'm';
                    break;
                case Keys.N:
                    result = 'n';
                    break;
                case Keys.O:
                    result = 'o';
                    break;
                #endregion
                #region P-T
                case Keys.P:
                    result = 'p';
                    break;
                case Keys.Q:
                    result = 'q';
                    break;
                case Keys.R:
                    result = 'r';
                    break;
                case Keys.S:
                    result = 's';
                    break;
                case Keys.T:
                    result = 't';
                    break;
                #endregion
                #region U-Z
                case Keys.U:
                    result = 'u';
                    break;
                case Keys.V:
                    result = 'v';
                    break;
                case Keys.W:
                    result = 'w';
                    break;
                case Keys.X:
                    result = 'x';
                    break;
                case Keys.Y:
                    result = 'y';
                    break;
                case Keys.Z:
                    result = 'z';
                    break;
                #endregion
                #region 0-9
                case Keys.D0:
                    result = '0';
                    break;
                case Keys.D1:
                    result = '1';
                    break;
                case Keys.D2:
                    result = '2';
                    break;
                case Keys.D3:
                    result = '3';
                    break;
                case Keys.D4:
                    result = '4';
                    break;
                case Keys.D5:
                    result = '5';
                    break;
                case Keys.D6:
                    result = '6';
                    break;
                case Keys.D7:
                    result = '7';
                    break;
                case Keys.D8:
                    result = '8';
                    break;
                case Keys.D9:
                    result = '9';
                    break;
                #endregion
                #region Numpad 0-9
                case Keys.NumPad0:
                    result = '0';
                    break;
                case Keys.NumPad1:
                    result = '1';
                    break;
                case Keys.NumPad2:
                    result = '2';
                    break;
                case Keys.NumPad3:
                    result = '3';
                    break;
                case Keys.NumPad4:
                    result = '4';
                    break;
                case Keys.NumPad5:
                    result = '5';
                    break;
                case Keys.NumPad6:
                    result = '6';
                    break;
                case Keys.NumPad7:
                    result = '7';
                    break;
                case Keys.NumPad8:
                    result = '8';
                    break;
                case Keys.NumPad9:
                    result = '9';
                    break;
                #endregion
                #region OEM punctuation and special characters
                case Keys.OemBackslash:
                    result = '\\';
                    break;
                case Keys.OemCloseBrackets:
                    result = ']';
                    break;
                case Keys.OemComma:
                    result = ',';
                    break;
                case Keys.OemMinus:
                    result = '-';
                    break;
                case Keys.OemOpenBrackets:
                    result = '[';
                    break;
                case Keys.OemPeriod:
                    result = '.';
                    break;
                case Keys.OemPlus:
                    result = '=';
                    break;
                case Keys.OemQuestion:
                    result = '/';
                    break;
                case Keys.OemQuotes:
                    result = '\'';
                    break;
                case Keys.OemSemicolon:
                    result = ';';
                    break;
                case Keys.OemTilde:
                    result = '`';
                    break;
                #endregion
                #region Numpad punctuation and special characters
                case Keys.Add:
                    result = '+';
                    break;
                case Keys.Subtract:
                    result = '-';
                    break;
                case Keys.Multiply:
                    result = '*';
                    break;
                case Keys.Divide:
                    result = '/';
                    break;
                case Keys.Decimal:
                    result = '.';
                    break;
                #endregion
                case Keys.Space:
                    result = ' ';
                    break;
                default:
                    result = '\0';
                    break;

            }
            //Handle capitalization.
            if (shifted ^ capsLock)
            {
                if (char.IsLetter(result))
                {
                    result = char.ToUpper(result);
                }
                else
                {

                    switch (result)
                    {
                        #region Numbers
                        case '0':
                            result = ')';
                            break;
                        case '1':
                            result = '!';
                            break;
                        case '2':
                            result = '@';
                            break;
                        case '3':
                            result = '#';
                            break;
                        case '4':
                            result = '$';
                            break;
                        case '5':
                            result = '%';
                            break;
                        case '6':
                            result = '^';
                            break;
                        case '7':
                            result = '&';
                            break;
                        case '8':
                            result = '*';
                            break;
                        case '9':
                            result = '(';
                            break;
                        #endregion
                        #region Punctuation
                        case '`':
                            result = '~';
                            break;
                        case '-':
                            result = '_';
                            break;
                        case '=':
                            result = '+';
                            break;
                        case '[':
                            result = '{';
                            break;
                        case ']':
                            result = '}';
                            break;
                        case '\\':
                            result = '|';
                            break;
                        case ';':
                            result = ':';
                            break;
                        case '\'':
                            result = '\"';
                            break;
                        case ',':
                            result = '<';
                            break;
                        case '.':
                            result = '>';
                            break;
                        case '/':
                            result = '?';
                            break;
                        #endregion
                    }
                }
            }

            return result;
        }


        #endregion
        #region Properties
        /// <summary>
        /// Gets whether either shift is pressed.
        /// </summary>
        public bool Shifted
        {
            get
            {
                foreach (sidhe3141utils.Xna.Action_and_Input.Action x in Actions)
                {
                    if (x.Data[0] == (object)Keys.LeftShift | x.Data[0] == (object)Keys.RightShift)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        #endregion
    }
}
