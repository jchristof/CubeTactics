using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game {
    public abstract class Condition {
        ConditionState _state;
        public Condition() {
            _state = ConditionState.Inactive;
        }

        public string Instructions { get; set; }

        public ConditionState State {
            get { return _state; }
            protected set {
                if (_state == value)
                    return;

                if (value == ConditionState.Inactive)
                    OnInactivate();
                else if (value == ConditionState.Active)
                    OnActivate();
                else if (value == ConditionState.Complete)
                    OnComplete();
                else if (value == ConditionState.Failed)
                    OnFail();

                _state = value;
            }
        }
        
        public Action OnInactivate = new Action(() => { });
        public Action OnActivate = new Action(() => { });
        public Action OnComplete = new Action(() => { });
        public Action OnFail = new Action(() => { });

        public abstract bool Required {
            get;
        }

        public bool Failed {
            get {
                return State == ConditionState.Failed;
            }
        }

        public bool Complete {
            get {
                return State == ConditionState.Complete;
            }
        }

        public bool Active {
            get {
                return State == ConditionState.Active;
            }
        }

        public void Activate() {
            State = ConditionState.Active;
        }

        public void Inactivate() {
            State = ConditionState.Inactive;
        }

        public abstract void ExecutePlayerMove(Vector3 position);
    }
}
