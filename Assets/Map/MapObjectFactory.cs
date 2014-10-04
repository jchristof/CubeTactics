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
        public Vector3 CreatePlayerSpawnPoint(IList<MapObject> mapObjects, IMap map) {
            var spawnPoint = 
                mapObjects
                    .Where(x => x.Name == "spawnplayer")
                    .Where(x => x.Type == MapObjectType.SpawnPoint)
                    .FirstOrDefault();

            if (spawnPoint == null)
                throw new InvalidOperationException("no player spawnpoint");

            return new Vector3(map.PixelXToTileX(spawnPoint.X), 0.5f, map.PixelXToTileX(spawnPoint.Y));
        }

        public IEnumerable<Assets.Map.Script.Script> CreateScripts(IList<MapObject> mapObjects) {
            return mapObjects
                    .Where(x => x.Type == MapObjectType.Script)
                    .Select(s => new Assets.Map.Script.Script {
                        Name = s.Name,
                        //Type = s.type,
                        //ScriptValue = s.properties.script
                    });
        }

        public IEnumerable<Trigger> CreateTeleporters(IMap map, IScriptExecutor scriptExecutor, IList<MapObject> mapObjects, ReadOnlyCollection<Trigger> triggersList) {
            var teleporters =
                    mapObjects
                        .Where(x => x.Type == MapObjectType.Teleporter);

            var newTeleporters = teleporters.Select(t => new Teleporter(map,  scriptExecutor));

            return newTeleporters.Cast<Trigger>();
        }

        public IEnumerable<Trigger> CreateEnterExitTriggers(IMap map, IScriptExecutor scriptExecutor, IList<MapObject> mapObjects, ReadOnlyCollection<Trigger> triggersList) {
            var enterExitTriggers =
                    mapObjects
                        .Where(x => x.Type == MapObjectType.EnterExit);


            var newEnterExitTriggers = enterExitTriggers.Select(t => new EnterExit(map, scriptExecutor));

            return newEnterExitTriggers.Cast<Trigger>();
        }

        public CommandList DeserializeScript(string scriptJson) {
            return JsonConvert.DeserializeObject<CommandList>(scriptJson);
        }

    }
}
