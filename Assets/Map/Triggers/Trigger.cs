using Assets.Script;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Map {
    public class Trigger {

        public Trigger(MapObject mapObject, IMap map, IScriptExecutor scriptExecutor){
            if (mapObject == null)
                throw new ArgumentNullException("mapObject");

            if (map == null)
                throw new ArgumentNullException("map");

            if (scriptExecutor == null)
                throw new ArgumentNullException("scriptExecutor");

            _mapObject = mapObject;
            _map = map;
            _scriptExecutor = scriptExecutor;
        }

        readonly MapObject _mapObject;
        readonly IMap _map;
        readonly IScriptExecutor _scriptExecutor;

        public virtual void OnEnter() { }
        public virtual void OnExit() { }

        protected void ExecuteScript(CommandList commandList) {
            _scriptExecutor.Execute(commandList);
        }

        public MapObject.Properties Properties { get { return _mapObject.properties; } }

        public int X { get { return _map.PixelXToTileX(_mapObject.x); } }
        public int Y { get { return _map.PixelYToTileY(_mapObject.y); } }
        public string Name { get { return _mapObject.name; } }
        public MapObjectType Type { get { return _mapObject.type; } }
        public bool Visible { get { return _mapObject.visible; } }
    }
}
