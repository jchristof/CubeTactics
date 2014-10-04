using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Map {
    [JsonConverter(typeof(MapObjectConverter))]
    public class MapObject {
        public class MapObjectProperties {
            public string LinkTo { get; set; }
            public bool Enabled { get; set; }
            public string OnEnter { get; set; }
            public string OnExit { get; set; }
            public int TileIndex { get; set; }

            [JsonConverter(typeof(StringEnumConverter))]
            public MapObjectType Type { get; set; }
            [JsonConverter(typeof(StringEnumConverter))]
            public MapObjectState State { get; set; }
            public string Name;
        }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("properties")]
        public MapObjectProperties Properties { get; set; }
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MapObjectType Type { get; set; }
        [JsonProperty("visible")]
        public bool Visible { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("x")]
        public int X { get; set; }
        [JsonProperty("y")]
        public int Y { get; set; }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void OnActivate() { }
        public virtual void OnDeactivate() { }
    }
}
