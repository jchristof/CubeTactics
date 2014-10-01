using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Movement.PlayerMovement {
    public class KeyInputDirection : IInputDirection {
        public void Update() {
        }

        public bool InputKeyLeft() { return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow); }
        public bool InputKeyUp() { return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow); }
        public bool InputKeyRight() { return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow); }
        public bool InputKeyDown() { return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow); }
    }
}
