using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Map.Triggers {
    public class Teleporter : Trigger{
        public Teleporter(string name, string type, int id, int xpos, int ypos, int linkTo, bool visible, bool enabled) :
            base(name, type, id, xpos, ypos, linkTo, visible, enabled) {
        }

        public List<Trigger> Triggers { get; set; }

        public override void OnTriggered() {
            Trigger destinationTrigger = Triggers.Find(x => x.Id == LinkTo);
            if (destinationTrigger != null) {
                CompositionRoot.PlayerController.AutoMatedMoveTo(new Vector3(destinationTrigger.X, 0.5f, destinationTrigger.Y));
            }
        }
    }
}
