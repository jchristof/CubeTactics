using Assets.Map.Triggers;
using Assets.Script;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Map {
    public class MapObjectFactory : IMapObjectFactory {
        public Vector3 CreatePlayerSpawnPoint(IList<MapLayerObject> mapObjects, IMap map) {
            var spawnPoint = 
                mapObjects
                    .Where(x => x.name == "spawnpoint")
                    .Where(x => x.type == "spawnplayer")
                    .FirstOrDefault();

            if (spawnPoint == null)
                throw new InvalidOperationException("no player spawnpoint");

            return new Vector3(map.PixelXToTileX(spawnPoint.x), 0.5f, map.PixelXToTileX(spawnPoint.y));
        }

        public IEnumerable<Assets.Map.Script.Script> CreateScripts(IList<MapLayerObject> mapObjects) {
            return mapObjects
                    .Where(x => x.name == "script")
                    .Select(s => new Assets.Map.Script.Script {
                        Name = s.name,
                        Type = s.type,
                        ScriptValue = s.properties.script
                    });
        }

        public IEnumerable<Trigger> CreateTeleporters(IMap map, IEnumerable<MapLayerObject> triggerdata, ReadOnlyCollection<Trigger> triggersList) {
            var teleporters =
                    triggerdata
                        .Where(x => x.type == "teleport");

            var newTeleporters = teleporters.Select(t=>new Teleporter(t.name,
                      t.type,
                      Convert.ToInt32(t.properties.id),
                      map.PixelXToTileX(t.x),
                      map.PixelYToTileY(t.y),
                      Convert.ToInt32(t.properties.linkto),
                      t.visible,
                      t.properties.enabled,
                      triggersList));

            return newTeleporters.Cast<Trigger>();
        }

        ObjectCommand[] DeserializeScript(string scriptJson) {
            return JsonConvert.DeserializeObject<ObjectCommand[]>(scriptJson);
        }

        public IEnumerable<Trigger> CreatePressurePlates(IMap map, IEnumerable<Assets.Map.Script.Script> scripts, IEnumerable<MapLayerObject> triggerdata) {
            var pressurePlates =
                    triggerdata
                        .Where(x => x.type == "pressureplate");

            var newPressurePlates = pressurePlates.Select(p => new PressurePlate(p.name,
                      p.type,
                      Convert.ToInt32(p.properties.id),
                      map.PixelXToTileX(p.x),
                      map.PixelYToTileY(p.y),
                      Convert.ToInt32(p.properties.linkto),
                      p.visible,
                      p.properties.enabled,
                      p.properties.script));

            foreach (var p in newPressurePlates) {
                var script = scripts.Where(x => x.Type == p.ScriptName).FirstOrDefault();
                if (script != null)
                    p.Script = JsonConvert.DeserializeObject<ObjectCommand[]>(script.ScriptValue);
            }
                
            return newPressurePlates.Cast<Trigger>();
        }
    }
}
