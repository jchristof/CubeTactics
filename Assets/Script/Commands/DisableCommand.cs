using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Commands {
    public class DisableCommand : Command {
        public override void Execute() {
            CompositionRoot.RunOnMainThread(() => {
                GameObject.Find(ObjectName).GetComponent<PlayerController>().enabled = false;
            });
        }
    }
}
