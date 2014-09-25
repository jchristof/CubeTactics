using Assets.Map;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game {
    public abstract class LevelConditions {

        public LevelConditions() {
            _conditions = new List<Condition>();
        }

        public void Add(Condition condition) {
            _conditions.Add(condition);
        }

        public List<Condition> Conditions {
            get { return _conditions; }
        }

        List<Condition> _conditions;

        public bool Failed { get; set; }
        
        public bool Complete { get; set; }
        

        public virtual void ExecutePlayerMove(Vector3 position) {
        }

        public virtual void Update() {
        }

        public void Evaluate() {
            foreach (Condition c in _conditions) {
                if (c.Failed && c.Required) {
                    Failed = true;
                    break;
                }
                else if(c.Complete)
                    Complete = true;

                else if (c.Active && c.Required) {
                    Complete = false;
                    break;
                }

            }
        }
    }
}
