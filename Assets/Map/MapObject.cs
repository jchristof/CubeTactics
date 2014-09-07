using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Map {
    public class MapObject {
        public class Properties {
            public string linkto { get; set; }
            public bool enabled { get; set; }
            public string onEnter { get; set; }
            public string onExit { get; set; }

            [JsonConverter(typeof(StringEnumConverter))]
            public MapObjectType type { get; set; }
            public string name;
        }

        public int height { get; set; }
        public string name { get; set; }
        public Properties properties { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public MapObjectType type { get; set; }
        public bool visible { get; set; }
        public int width { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }
}
