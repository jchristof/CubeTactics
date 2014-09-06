using Assets.Script;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace Assets.Map {
    interface IMapObjectFactory {
        UnityEngine.Vector3 CreatePlayerSpawnPoint(System.Collections.Generic.IList<MapObject> mapObjects, IMap map);
        IEnumerable<Assets.Map.Script.Script> CreateScripts(IList<MapObject> mapObjects);
        IEnumerable<Trigger> CreateTeleporters(IMap map, Assets.Script.IScriptExecutor scriptExecutor, IList<MapObject> mapObjects, ReadOnlyCollection<Trigger> triggersList);
        CommandList DeserializeScript(string scriptJson);
        IEnumerable<Trigger> CreateEnterExitTriggers(IMap map, IScriptExecutor scriptExecutor, IList<MapObject> mapObjects, ReadOnlyCollection<Trigger> triggersList);
    }
}
