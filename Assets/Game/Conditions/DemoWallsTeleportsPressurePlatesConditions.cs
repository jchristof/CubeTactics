using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Game.Conditions {
    class DemoWallsTeleportsPressurePlatesConditions : LevelConditions {
        public DemoWallsTeleportsPressurePlatesConditions()
            : base() {

            SpecificTileCondition s = new SpecificTileCondition("goal", "Goal");
            s.Instructions = "Make it to the goal";
            Conditions.Add(s);
            s.Activate();
        }
    }
}
