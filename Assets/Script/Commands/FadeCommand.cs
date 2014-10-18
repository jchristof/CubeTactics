using Assets.Actors;
using Assets.Script.Positioning;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Commands {
    [JsonConverter(typeof(CommandConverter))]
    public class FadeCommand : Command {
        public Direction Direction { get; set; }
        public int Milliseconds { get; set; }

        public override void Execute() {
            CompositionRoot.RunOnMainThread(() => {
                IActor actor = base.Identifier.ActorFor();
                actor.SetFade(Direction == Script.Direction.In ? 1.0f : 0.0f); 
            });
        }
    }
}
