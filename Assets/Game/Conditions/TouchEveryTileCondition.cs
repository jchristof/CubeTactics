using Assets.Language;
using Assets.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game.Conditions {
    public class TouchEveryTileCondition : Condition {

        public TouchEveryTileCondition()
            : base() {
                _unvisitedTiles = new List<Point<int>>();
                CompositionRoot.Map.ForeachTile(new Action<int, int, int>(InspectBoard), MapLayerName.Board);
                Instructions = "Touch every tile";
        }

        List<Point<int>> _unvisitedTiles;

        void InspectBoard(int x, int y, int tileIndex) {
            _unvisitedTiles.Add(new Point<int>(x, y));
        }
        public override bool Required {
            get { return true; }
        }

        public override void ExecutePlayerMove(UnityEngine.Vector3 position) {
            foreach (var p in _unvisitedTiles) {
                if (p.X == Convert.ToInt32(position.x) && p.Y == Convert.ToInt32(position.z)) {
                    _unvisitedTiles.Remove(p);
                    break;
                }
            }

            if(_unvisitedTiles.Count == 0)
                State = ConditionState.Complete;
        }
    }
}
