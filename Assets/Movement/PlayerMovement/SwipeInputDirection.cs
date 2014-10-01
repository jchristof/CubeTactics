using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Movement.PlayerMovement {
    public class SwipeInputDirection : IInputDirection{
        Swipe _swipe;

        public SwipeInputDirection() {
            _swipe = new Swipe();
        }

        public void Update() {
            _swipe.Update();
        }

        public bool InputKeyLeft() {
            return _swipe.SwipeType == SwipeType.Left;
        }

        public bool InputKeyUp() {
            return _swipe.SwipeType == SwipeType.Up;
        }

        public bool InputKeyRight() {
            return _swipe.SwipeType == SwipeType.Right;
        }

        public bool InputKeyDown() {
            return _swipe.SwipeType == SwipeType.Down;
        }
    }
}
