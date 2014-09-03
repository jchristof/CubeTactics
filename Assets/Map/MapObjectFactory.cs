using System;
using System.Collections.Generic;
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
    }
}
