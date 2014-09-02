using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Map {
    public class MapModel {
        public int height { get; set; }
        public IList<MapLayer> layers { get; set; }
        public string orientation { get; set; }
        public PropertiesMap properties { get; set; }
        //public int tileheight { get; set; }
        public IList<Tileset> tilesets { get; set; }
        public int tilewidth { get; set; }
        public int version { get; set; }
        public int width { get; set; }
    }
}
