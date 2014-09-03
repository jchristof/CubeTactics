using Assets.Script;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Map.Triggers {
    public class PressurePlate : Trigger{
        public PressurePlate(string name, string type, int id, int xpos, int ypos, int linkTo, bool visible, bool enabled, string scriptName) :
            base(name, type, id, xpos, ypos, linkTo, visible, enabled) {
                ScriptName = scriptName;
        }

        public string ScriptName { get; set; }
        public ObjectCommand[] Script { get; set; }

        public override void OnTriggered() {
            base.OnTriggered();
        }
    }
}
