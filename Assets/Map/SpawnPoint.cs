using Assets.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Map {
    public class SpawnPoint : Trigger {
        public SpawnPoint(IMap map, IScriptExecutor scriptExecutor)
            : base(map, scriptExecutor) {
        }
        public override Vector3 Position {
            get {
                return new Vector3(MapX, 0.0f, MapY);
            }
        }
    }
}
