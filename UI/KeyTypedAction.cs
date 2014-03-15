using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using sidhe3141utils.Xna.Action_and_Input;

namespace sidhe3141utils.Xna.UI
{
    class KeyTypedAction:GeneralAction
    {
        public KeyTypedAction(Keys key) : base(new object[] { key }, true) { }
    }
}
