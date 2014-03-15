using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sidhe3141utils.Xna.Stage;
using sidhe3141utils.Xna.GameObject;

namespace sidhe3141utils.Xna.Action_and_Input
{
    class ObjectAddAction:GeneralAction
    {
        public ObjectAddAction(GameObject.GameObject added, Stage.Stage addedTo) : base(new object[] { added, addedTo }, false) { }
    }
}
