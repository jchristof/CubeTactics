using Assets.Camera;
using Assets.Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Assets.Script {
    public class ScriptExecutor : UnityEngine.MonoBehaviour, IScriptExecutor  {
        IMap _map;

        public IMap Map {
            set { _map = value; }
        }

        public void Execute(IList<Command> commandList){
            if (commandList == null)
                return;

            var t = new Thread(() => {
                commandList.ToList().ForEach(x => { x.Execute(); Thread.Sleep(1); });
            });
            t.Start();
        }
    }
}
