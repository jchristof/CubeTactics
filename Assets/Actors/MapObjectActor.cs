using Assets.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Actors {
    public class MapObjectActor : IActor {

        public MapObjectActor(MapObject mapObject) {
            _mapObject = mapObject;
        }

        MapObject _mapObject;


        public void SetPosition(UnityEngine.Vector3 position) {
            throw new NotImplementedException();
        }

        public UnityEngine.Vector3 GetPosition() {
            return _mapObject.Position;
        }

        public void SetFade(float fade) {
            throw new NotImplementedException();
        }
    }
}
