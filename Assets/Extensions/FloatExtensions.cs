using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Extensions {
    static public class FloatExtensions {
        static public bool ApproximatelyEquals(this float value1, float value2, float acceptableDifference) {
            return Math.Abs(value1 - value2) <= acceptableDifference;
        }
    }
}
