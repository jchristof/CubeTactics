using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Positioning {
    public class Position {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { set; get; }

        public Vector3 Vector {
            get { return new Vector3(X, Y, Z); }
        }
    }
}
