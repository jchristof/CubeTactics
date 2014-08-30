using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Map {
    public class Tileset {
        public int firstgid { get; set; }
        public string image { get; set; }
        public int imageheight { get; set; }
        public int imagewidth { get; set; }
        public int margin { get; set; }
        public string name { get; set; }
        //public Properties2 properties { get; set; }
        public int spacing { get; set; }
        public int tileheight { get; set; }
        public JObject tileproperties { get; set; }
        public int tilewidth { get; set; }
    }
}
