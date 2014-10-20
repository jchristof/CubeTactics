using Assets.Map;
using Assets.Script.Positioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Commands {
    public class PlayerMoveCommand : Command {
        public PlayerMoveCommand(IMap map) {
            _map = map;
        }

        readonly IMap _map;

        public Indentifier DestinationObject { get; set; }
        //public Position Position { get; set; }

        public override void Execute() {
            CompositionRoot.RunOnMainThread(() => {
                Vector3 position = DestinationObject.PositionOf(_map);
                CompositionRoot.PlayerController.AutoMatedMoveTo(_map.PixelPositionToMapPosition(position.x, 0.0f, position.y));
            });
        }
    }
}
