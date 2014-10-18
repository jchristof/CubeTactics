using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Commands {
    [JsonConverter(typeof(CommandConverter))]
    public class EnableCommand : Command {
        public override void Execute() {
            CompositionRoot.RunOnMainThread(() => {
                GameObject.Find(Identifier.ObjectName).GetComponent<PlayerController>().enabled = true;
            });
        }
    }
}
