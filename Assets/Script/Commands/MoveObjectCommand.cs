using Assets.Actors;
using Assets.Map;
using Assets.Script.Positioning;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Commands {
    [JsonConverter(typeof(CommandConverter))]
    public class MoveObjectCommand : Command {
        public MoveObjectCommand(IMap map) {
            if(map == null)
                throw new ArgumentNullException("map");

            _map = map;
        }

        readonly IMap _map;

        public Position Position { get; set; }
        public Indentifier DestinationObject { get; set; }

        public override void Execute() {
            CompositionRoot.RunOnMainThread(() => {
                IActor actor = base.Identifier.ActorFor();
                actor.SetPosition(DestinationObject.PositionOf(_map) + Position.Vector);
            });
        }
    }
}
