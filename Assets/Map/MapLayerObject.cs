using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Map {
    public class MapLayerObject {
        public class Properties {
            public string linkto { get; set; }
            public string id { get; set; }
            public bool enabled { get; set; }
        }

        public int height { get; set; }
        public string name { get; set; }
        public Properties properties { get; set; }
        public string type { get; set; }
        public bool visible { get; set; }
        public int width { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }
}
