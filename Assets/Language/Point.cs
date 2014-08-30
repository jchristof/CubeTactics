using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Language {
    public class Point<T> {
        public Point(T x, T y) {
            X = x;
            Y = y;
        }

        public T X { get; set; }
        public T Y { get; set; }
    }
}
