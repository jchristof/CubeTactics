using Assets.Map.Triggers;
using Assets.Script;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Map {
    public class Trigger : MapObject {

        public Trigger(IMap map, IScriptExecutor scriptExecutor){
            if (map == null)
                throw new ArgumentNullException("map");

            if (scriptExecutor == null)
                throw new ArgumentNullException("scriptExecutor");

            _map = map;
            _scriptExecutor = scriptExecutor;
        }
        
        readonly IMap _map;
        readonly IScriptExecutor _scriptExecutor;



        protected void ExecuteScript(IList<Command> commandList) {
             _scriptExecutor.Execute(commandList);
        }

        public IMap Map { get { return _map; } }

        public int MapX { 
            get { return _map.PixelXToTileX(base.X); }
        }
        public int MapY { 
            get { return _map.PixelYToTileY(base.Y); }

        }
    }
}
