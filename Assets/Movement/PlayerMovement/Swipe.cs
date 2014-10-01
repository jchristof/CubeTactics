using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Movement.PlayerMovement {
    public class Swipe {
        float minSwipeDistY = 10.0f;
        float minSwipeDistX = 10.0f;
        Vector2 startPos;
        TouchAbstractor _touch = new TouchAbstractor();

        public SwipeType SwipeType { get; set; }

        public void Update() {
            _touch.Update();
#if UNITY_ANDROID
            if (Input.touchCount > 0) {
#else
            if (true) {
#endif
                TouchAbstractor.TouchInternal touch = _touch.Touch;
                //Debug.Log("Touch phase: " + touch.phase.ToString());
                switch (touch.phase) {

                    case TouchPhase.Began:

                        startPos = touch.position;
                        break;

                    case TouchPhase.Ended:
                        
                        float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
                        float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;

                        if (swipeDistVertical > minSwipeDistY && swipeDistVertical > swipeDistHorizontal) {
                            float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
                            if (swipeValue > 0) {
                                SwipeType = PlayerMovement.SwipeType.Up;
                                //Debug.Log("swipe up");
                            }
                            else if (swipeValue < 0) {
                                SwipeType = PlayerMovement.SwipeType.Down;
                                //Debug.Log("swipe down");
                            }

                            return;
                        }

                        
                        if (swipeDistHorizontal > minSwipeDistX && swipeDistHorizontal > swipeDistVertical) {
                            float swipeValue = Mathf.Sign(touch.position.x - startPos.x);

                            if (swipeValue > 0) {
                                SwipeType = PlayerMovement.SwipeType.Right;
                                //Debug.Log("swipe right");
                            }

                            else if (swipeValue < 0) {
                                SwipeType = PlayerMovement.SwipeType.Left;
                                //Debug.Log("swipe left");
                            }

                            return;

                        }
                        break;
                }

                SwipeType = PlayerMovement.SwipeType.None;
            }
        }
    }
}

