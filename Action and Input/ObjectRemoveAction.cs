using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sidhe3141utils.Xna.Stage;
using sidhe3141utils.Xna.GameObject;

namespace sidhe3141utils.Xna.Action_and_Input
{
    class ObjectRemoveAction:GeneralAction
    {
        public ObjectRemoveAction(GameObject.GameObject removed, Stage.Stage removedFrom) : base(new object[] { removed, removedFrom }, false) { }
    }
}
