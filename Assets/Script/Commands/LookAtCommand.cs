using Assets.Camera;
using Assets.Map;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Commands {
    [JsonConverter(typeof(CommandConverter))]
    public class LookAtCommand : Command {
        public LookAtCommand() { }
        public LookAtCommand(IMap map) {
            if(map == null)
                throw new ArgumentNullException("map");

            _map = map;
        }

        readonly IMap _map;

        public override void Execute() {
            CompositionRoot.RunOnMainThread(() => {
                MapObject mapObject = _map.MapObjects.Where(x => x.Name == Identifier.ObjectName).FirstOrDefault();
                //_map.MapObjects.ToList().ForEach(x => { print(x.Name); });

                if (mapObject != null) {
                    //if (mapObject.type == MapObjectType.Tile) {
                    int x = _map.PixelXToTileX(mapObject.X);
                    int y = _map.PixelYToTileY(mapObject.Y);

                    GameObject go = GameObject.Find(string.Format("{0}", _map.FlatTileIndex(x, y)));
                    UnityEngine.Camera.main.GetComponent<CameraScript>().Target = go.transform;
                    //}
                }
                else {
                    GameObject go = GameObject.Find(Identifier.ObjectName);
                    UnityEngine.Camera.main.GetComponent<CameraScript>().Target = go.transform;
                    //print(string.Format("Look at tile name {0}, flat index {1}", cmd.ObjectName, go.name));
                }
            });
        }
    }
}
