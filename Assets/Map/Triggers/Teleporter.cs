using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Map.Triggers {
    public class Teleporter : Trigger{
        public Teleporter(IMap map, MapObject mapObject, ReadOnlyCollection<Trigger> triggers) :
            base(mapObject, map) {
                Triggers = triggers;
        }

        public ReadOnlyCollection<Trigger> Triggers { get; private set; }

        public override void OnEnter() {
            Trigger destinationTrigger = Triggers.Where(x => x.Name == Properties.linkto).FirstOrDefault();
            if (destinationTrigger != null) {
                CompositionRoot.PlayerController.AutoMatedMoveTo(new Vector3(destinationTrigger.X, 0.5f, destinationTrigger.Y));
            }
        }

        public override void OnExit() {
            throw new NotImplementedException();
        }
    }
}
