﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Map {
    public class MapLayer {
        public IList<int> data { get; set; }
        public int height { get; set; }
        public string name { get; set; }
        public int opacity { get; set; }
        public string type { get; set; }
        public bool visible { get; set; }
        public int width { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public IList<MapLayerObject> objects { get; set; }
    }

}
