using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Actors {
    public class GameObjectActor : IActor {
        public GameObjectActor(GameObject gameObject) {
            _gameObject = gameObject;
        }

        GameObject _gameObject;

        public void SetPosition(UnityEngine.Vector3 position) {
            _gameObject.transform.position = position;
        }

        public Vector3 GetPosition() {
            return _gameObject.transform.position;
        }

        public void SetFade(float alpha) {
            Color color = _gameObject.transform.renderer.material.color;
            color.a = alpha;
            _gameObject.transform.renderer.material.color = color;
        }
    }
}
