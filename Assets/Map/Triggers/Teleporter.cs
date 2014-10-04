using Assets.Script;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Map.Triggers {
    public class Teleporter : Trigger{
        public Teleporter(IMap map, IScriptExecutor scriptExecutor) :
            base(map, scriptExecutor) {
        }

        public override void OnEnter() {
            Trigger destinationTrigger = (Trigger)Map.MapObjects.Where(x => x.Name == Properties.LinkTo).FirstOrDefault();
            if (destinationTrigger != null) {
                CompositionRoot.PlayerController.AutoMatedMoveTo(new Vector3(destinationTrigger.MapX, 0.5f, destinationTrigger.MapY));
            }
        }
    }
}
