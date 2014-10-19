using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Actors {
    public class GameObjectActor : IActor {
        public GameObjectActor(GameObject gameObject) {
            _gameObjectRef = new WeakReference(gameObject);
        }

        WeakReference _gameObjectRef;

        public void SetPosition(UnityEngine.Vector3 position) {
            if(_gameObjectRef.IsAlive)
                ((GameObject)_gameObjectRef.Target).transform.position = position;
        }

        public Vector3 GetPosition() {
            if (!_gameObjectRef.IsAlive)
                return Vector3.zero;
            return ((GameObject)_gameObjectRef.Target).transform.position;
        }

        public void SetFade(float alpha) {
            if (!_gameObjectRef.IsAlive)
                return;
            Color color = ((GameObject)_gameObjectRef.Target).transform.renderer.material.color;
            color.a = alpha;
            ((GameObject)_gameObjectRef.Target).transform.renderer.material.color = color;
        }
    }
}
