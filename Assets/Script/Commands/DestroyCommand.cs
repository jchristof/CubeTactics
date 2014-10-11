using Assets.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Commands {
    public class DestroyCommand : Command {
        public DestroyCommand() { }
        public DestroyCommand(IMap map) {
            if(map == null)
                throw new ArgumentNullException("map");

            _map = map;
        }

        readonly IMap _map;
        public override void Execute() {
            CompositionRoot.RunOnMainThread(() => {
                MapObject mapObject = _map.MapObjects.Where(x => x.Name == ObjectName).FirstOrDefault();
                if (mapObject == null) {
                    GameObject go = GameObject.Find(ObjectName);
                    GameObject.Destroy(go);
                }
                else if (mapObject.Type == MapObjectType.Tile) {
                    int x = _map.PixelXToTileX(mapObject.X);
                    int y = _map.PixelYToTileY(mapObject.Y);
                    _map.RemoveTileAt(x, y, MapLayerName.Board);
                    CompositionRoot.Playfield.RemoveTileVisualAt(new Vector3(x, 0, y));
                }
                else {
                    throw new NotImplementedException();
                }
            });
        }
    }
}
