using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sidhe3141utils.Xna;
using sidhe3141utils.Xna.Action_and_Input;

namespace sidhe3141utils.Xna.UI
{
    class ControlClickedAction:GeneralAction
    {
                public ControlClickedAction(UIElement element) : base(new object[] { element }, true) { }
    }
}
