using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Assets.Movement.PlayerMovement {
    public class TouchAbstractor {

        public struct TouchInternal {

            public Vector2 deltaPosition { get; set; }
            public float deltaTime { get; set; }
            public int fingerId { get; set; }
            public TouchPhase phase { get; set; }
            public Vector2 position { get; set; }
            public Vector2 rawPosition { get; set; }
            public int tapCount { get; set; }

            public void Set(Touch touch) {
                deltaPosition = touch.deltaPosition;
                deltaTime = touch.deltaTime;
                fingerId = touch.fingerId;
                phase = touch.phase;
                position = touch.position;
                rawPosition = touch.rawPosition;
                tapCount = touch.tapCount;
            }
        }

        TouchInternal _touch = new TouchInternal();
        public TouchInternal Touch {
            get {
                return _touch;
            }
        }

        public void Update() {
#if UNITY_ANDROID
            if (Input.touchCount > 0) {
                string touchJson = JsonConvert.SerializeObject(Input.touchCount);
                Debug.Log(touchJson);
                _touch.Set(Input.touches[0]);
            }
#else
            if(_touch.phase == TouchPhase.Began){
                if(Input.GetButtonUp("Fire1")){
                    _touch.phase = TouchPhase.Ended;
                    _touch.position = Input.mousePosition;
                    return;
                }
                return;
            }
            if (Input.GetButtonDown("Fire1") && _touch.phase != TouchPhase.Began) {
                _touch.phase = TouchPhase.Began;
                _touch.position = Input.mousePosition;
                return;
            }

            _touch.phase = TouchPhase.Stationary;
#endif

        }
    }
}
