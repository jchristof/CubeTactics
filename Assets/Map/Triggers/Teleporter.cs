using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Map.Triggers {
    public class Teleporter : Trigger{
        public Teleporter(string name, string type, int id, int xpos, int ypos, int linkTo, bool visible, bool enabled, ReadOnlyCollection<Trigger> triggers) :
            base(name, type, id, xpos, ypos, linkTo, visible, enabled) {
                Triggers = triggers;
        }

        public ReadOnlyCollection<Trigger> Triggers { get; private set; }

        public override void OnTriggered() {
            Trigger destinationTrigger = Triggers.Where(x => x.Id == LinkTo).FirstOrDefault();
            if (destinationTrigger != null) {
                CompositionRoot.PlayerController.AutoMatedMoveTo(new Vector3(destinationTrigger.X, 0.5f, destinationTrigger.Y));
            }
        }
    }
}
