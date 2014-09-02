using Assets.Script;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Map.Triggers {
    public class PressurePlate : Trigger{
        public PressurePlate(string name, string type, int id, int xpos, int ypos, int linkTo, bool visible, bool enabled) :
            base(name, type, id, xpos, ypos, linkTo, visible, enabled) {
        }

        public ObjectCommand[] Script { get; set; }

        public override void OnTriggered() {
            base.OnTriggered();
        }
    }
}
