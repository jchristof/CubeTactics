using Assets.Map;
using Assets.Script.Positioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Commands {
    public class MovePlayerCommand : Command {
        public MovePlayerCommand(IMap map) {
            _map = map;
        }

        readonly IMap _map;

        public Indentifier DestinationObject { get; set; }
        //public Position Position { get; set; }

        public override void Execute() {
            CompositionRoot.RunOnMainThread(() => {
                Vector3 position = DestinationObject.PositionOf(_map);
                CompositionRoot.PlayerController.AutoMatedMoveTo(_map.PixelPositionToMapPosition(position.x, 0.5f, position.y));
            });
        }
    }
}
