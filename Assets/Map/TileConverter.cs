using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Map {
    public class TileConverter : JsonConverter {

        public override bool CanConvert(Type objectType) {
            return  objectType == typeof(Tile);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            JObject jObject = JObject.Load(reader);
            Tile target = new Tile();

            //var typeString = (string)jObject.Property("ObjectCommand");
            //var type = (ObjectCommand)Enum.Parse(typeof(ObjectCommand), typeString);

            //switch (type) {

            //    case ObjectCommand.Activate:
            //        target = new ActivateCommand(_map);
            //        break;

            //    case ObjectCommand.Create:
            //        target = new CreateCommand(_map);
            //        break;

            //    case ObjectCommand.Destroy:
            //        target = new DestroyCommand(_map);
            //        break;

            //    case ObjectCommand.Disable:
            //        target = new DisableCommand();
            //        break;

            //    case ObjectCommand.Enable:
            //        target = new EnableCommand();
            //        break;

            //    case ObjectCommand.LookAt:
            //        target = new LookAtCommand(_map);
            //        break;

            //    case ObjectCommand.Sfx:
            //        target = new SfxCommand();
            //        break;

            //    case ObjectCommand.Wait:
            //        target = new WaitCommand();
            //        break;

            //    default:
            //        target = new Command();
            //        throw new NotImplementedException("CommandConverter");
            //        break;
            //}


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
