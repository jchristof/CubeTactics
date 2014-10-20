using Assets.Map.Triggers;
using Assets.Script;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Map {
    class MapObjectConverter : JsonConverter {

        public override bool CanConvert(Type objectType) {
            return objectType.IsSubclassOf(typeof(MapObject)) || objectType == typeof(MapObject);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            JObject jObject = JObject.Load(reader);
            MapObject target;
            var type = (string)jObject.Property("type");

            switch (type) {
                case "EnterExit":
                    target = new EnterExit(CompositionRoot.Map, CompositionRoot.ScriptExecutor);
                    break;

                case "SpawnPoint":
                    target = new SpawnPoint(CompositionRoot.Map, CompositionRoot.ScriptExecutor);
                    break;

                case "Goal":
                    target = new Goal(CompositionRoot.Map, CompositionRoot.ScriptExecutor);
                    break;

                default:
                    target = new MapObject();
                    break;
            }


            serializer.Populate(jObject.CreateReader(), target);
            return target;
        }

        public override bool CanWrite {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }
    }
}
