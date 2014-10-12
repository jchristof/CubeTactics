using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Map {
     [JsonConverter(typeof(TileConverter))]
    public class Tile {
        public int index { get; set; }
        public string type {get;set;}
        public string value { get; set;}

        public override string ToString() {
            return string.Format("Tile index {0}, type: {1}, value: {2}", index, type, value);
        }
    }
}
