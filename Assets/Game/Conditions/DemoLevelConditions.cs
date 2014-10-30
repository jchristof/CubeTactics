using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Conditions {
    public class DemoLevelConditions : LevelConditions {

        public DemoLevelConditions() : base() {
            NumericSequenceCondition n = new NumericSequenceCondition(1, 4);
            n.Activate();

            Conditions.Add(n);
            SpecificTileCondition s = new SpecificTileCondition("trigger", "goal");
            s.Instructions = "Make it to the goal";
            Conditions.Add(s);

            n.OnComplete = new Action(()=>{
                n.Inactivate();        
                s.Activate();                
            });

            n.OnFail = new Action(() => {
                n.Inactivate();
                s.Activate();
            });     
        }
    }
}
