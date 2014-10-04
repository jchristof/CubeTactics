using Assets.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Map.Triggers {
    public class Goal : Trigger {
        public Goal(IMap map, IScriptExecutor scriptExecutor) : base(map, scriptExecutor) {
        }

        public override void OnActivate() {
            base.OnActivate();

            GameObject goal = GameObject.Find("Goal");
            goal.GetComponent<MeshRenderer>().enabled = true;
            goal.transform.position = new Vector3(MapX, 0.5f, MapY);

            goal.GetComponent<ParticleSystem>().loop = true;
            goal.GetComponent<ParticleSystem>().Play();

        }
    }
}
