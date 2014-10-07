using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Commands {
    public class CommandConverter : JsonConverter {

        public override bool CanConvert(Type objectType) {
            return objectType.IsSubclassOf(typeof(Command)) || objectType == typeof(Command);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            JObject jObject = JObject.Load(reader);
            Command target;
            var type = (string)jObject.Property("type");

            switch (type) {
                case "Create":
                    target = new CreateCommand(CompositionRoot.Map);
                    break;

                case "Destroy":
                    target = new DestroyCommand(CompositionRoot.Map);
                    break;

                case "Disable":
                    target = new DisableCommand();
                    break;

                case "Enable":
                    target = new EnableCommand();
                    break;

                case "LookAt":
                    target = new LookAtCommand(CompositionRoot.Map);
                    break;

                case "Sfx":
                    target = new SfxCommand();
                    break;

                default:
                    target = new Command();
                    throw new NotImplementedException("CommandConverter");
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
