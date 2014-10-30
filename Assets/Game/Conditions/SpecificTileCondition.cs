using Assets.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Game.Conditions {
    public class SpecificTileCondition : Condition {
        public SpecificTileCondition(string type, string value) {
            _type = type;
            _value = value;
        }

        string _type;
        string _value;

        public override bool Required {
            get { return true; }
        }

        public override void ExecutePlayerMove(UnityEngine.Vector3 position) {
            if (State != ConditionState.Active)
                return;

            Tile t = CompositionRoot.Map.TileAtMapPosition(position, MapLayerName.Board);
            if (t.type == _type && t.value == _value)
                State = ConditionState.Complete;
        }
    }
}
