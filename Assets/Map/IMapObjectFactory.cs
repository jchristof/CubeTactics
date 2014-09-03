using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Map {
    public interface IMapObjectFactory {
        Vector3 CreatePlayerSpawnPoint(IList<MapLayerObject> mapObjects, IMap map);
    }
}
