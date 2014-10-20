using Assets.Script;
using Assets.Script.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Assets.Map.Triggers {
    public class EnterExit : Trigger {

        public EnterExit(IMap map, IScriptExecutor scriptExecutor) :
            base(map, scriptExecutor) {
        }

        public override void OnEnter() {
            base.OnEnter();

            if(!string.IsNullOrEmpty(Properties.OnEnter)){
                ExecuteScript(Properties.OnEnter);
            }   
        }
    }
}
