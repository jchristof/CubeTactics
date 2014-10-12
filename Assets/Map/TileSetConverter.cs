using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Map {
    class TileSetConverter : JsonConverter {

        public override bool CanConvert(Type objectType) {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            JObject jObject = JObject.Load(reader);

            IList<Tile> tilelist = new List<Tile>();
            foreach (var v in jObject) {
                Tile t = JsonConvert.DeserializeObject<Tile>(v.Value.ToString());
                t.index = Convert.ToInt32(v.Key);
                tilelist.Add(t);
            }

            //Tile target = new Tile();
            //serializer.Populate(jObject.CreateReader(), target);
            return tilelist;
        }

        public override bool CanWrite {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }
    }
}
