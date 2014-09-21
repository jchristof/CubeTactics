using Assets.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game {
    public class NumericSequenceCondition : Condition {

        public NumericSequenceCondition(int start, int end)
            : base() {
                SetRange(start, end);
                Instructions = "Cross the number tiles in order";
        }

        int _start;
        int _end;
        int _next;

        void SetRange(int start, int end) {
            _start = start;
            _end = end;
            _next = _start;
        }

        public override void ExecutePlayerMove(UnityEngine.Vector3 position) {
            if (State != ConditionState.Active)
                return;

            Tile t = CompositionRoot.Map.TileAtMapPosition(position, MapLayerName.Board);

            if (t.type == "number") {
                int number = Convert.ToInt32(t.value);

                if (number != _next) {
                    MonoBehaviour.print(string.Format(this.GetType().ToString() + " failed"));
                    this.State = ConditionState.Failed;
                    return;
                } 
                else if (_next == _end) {
                    MonoBehaviour.print(string.Format(this.GetType().ToString() + " complete"));
                    this.State = ConditionState.Complete;
                } 
                else {
                    MonoBehaviour.print(string.Format(this.GetType().ToString() + " condition sequence update got #{0} of {1} to {2}", number, _start, _end));
                    _next++;
                }
            }
        }

        public override bool Required {
            get { return true; }
        }
    }
}
