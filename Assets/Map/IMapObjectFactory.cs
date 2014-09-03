using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Map {
    public interface IMapObjectFactory {
        Vector3 CreatePlayerSpawnPoint(IList<MapLayerObject> mapObjects, IMap map);
        IEnumerable<Assets.Map.Script.Script> CreateScripts(IList<MapLayerObject> mapObjects);
        IEnumerable<Trigger> CreateTeleporters(IMap map, IEnumerable<MapLayerObject> triggerdata, ReadOnlyCollection<Trigger> triggersList);
        IEnumerable<Trigger> CreatePressurePlates(IMap map, IEnumerable<Assets.Map.Script.Script> scripts, IEnumerable<MapLayerObject> triggerdata);
    }
}
