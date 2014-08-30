using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Conditions {
    public class TouchEveryTileLevelConditions : LevelConditions {

        public TouchEveryTileLevelConditions()
            : base() {
                TouchEveryTileCondition t = new TouchEveryTileCondition();
            t.Activate();

            Conditions.Add(t);
            SpecificTileCondition s = new SpecificTileCondition("trigger", "goal");
            s.Instructions = "Make it to the goal";
            Conditions.Add(s);

            t.OnComplete = new Action(()=>{
                t.Inactivate();        
                s.Activate();                
            });

            t.OnFail = new Action(() => {
                t.Inactivate();
                s.Activate();
            });     
        }

        public override void ExecutePlayerMove(Vector3 position) {
            foreach (Condition c in Conditions) {
                c.ExecutePlayerMove(position);
            }
        }
    }
}
