using System;
namespace Assets.Map {
    interface IMapObjectFactory {
        UnityEngine.Vector3 CreatePlayerSpawnPoint(System.Collections.Generic.IList<MapObject> mapObjects, IMap map);
        System.Collections.Generic.IEnumerable<Assets.Map.Script.Script> CreateScripts(System.Collections.Generic.IList<MapObject> mapObjects);
        System.Collections.Generic.IEnumerable<Trigger> CreateTeleporters(IMap map, Assets.Script.IScriptExecutor scriptExecutor, System.Collections.Generic.IList<MapObject> mapObjects, System.Collections.ObjectModel.ReadOnlyCollection<Trigger> triggersList);
        Assets.Script.CommandList DeserializeScript(string scriptJson);
    }
}
