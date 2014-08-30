using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Extensions {
    public static class RectExtensions {
        public static Rect CenterIn(this Rect insetRect,  Rect outerRect) {
            Vector2 center = outerRect.center;

            insetRect.Set(
                center.x - (insetRect.width / 2) + insetRect.x, 
                center.y - (insetRect.y / 2) + insetRect.y, 
                insetRect.width, 
                insetRect.height
            );

            return insetRect;
        }
    }
}
