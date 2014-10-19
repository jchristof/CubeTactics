using Assets.Script;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptBuilder.ScriptCommands.Commands {
    public class CommandConverter : JsonConverter {

        public override bool CanConvert(Type objectType) {
            return objectType.IsSubclassOf(typeof(Command)) || objectType == typeof(Command);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            JObject jObject = JObject.Load(reader);

            //var type = (ObjectCommand)jObject.Property("ObjectCommand").Value.Value<Int64>();
            var typeString = (string)jObject.Property("ObjectCommand");
            var type = (ObjectCommand)Enum.Parse(typeof(ObjectCommand), typeString);

            switch (type) {

                case ObjectCommand.Activate:
                    ActivateCommand activateCommand = new ActivateCommand();
                    serializer.Populate(jObject.CreateReader(), activateCommand);
                    return activateCommand;

                case ObjectCommand.Create:
                    CreateCommand createCommand = new CreateCommand();
                    serializer.Populate(jObject.CreateReader(), createCommand);
                    return createCommand;

                case ObjectCommand.Destroy:
                    DestroyCommand destroyCommand = new DestroyCommand();
                    serializer.Populate(jObject.CreateReader(), destroyCommand);
                    return destroyCommand;

                case ObjectCommand.Disable:
                    DisableCommand disableCommand = new DisableCommand();
                    serializer.Populate(jObject.CreateReader(), disableCommand);
                    return disableCommand;

                case ObjectCommand.Enable:
                    EnableCommand enableCommand = new EnableCommand();
                    serializer.Populate(jObject.CreateReader(), enableCommand);
                    return enableCommand;

                case ObjectCommand.LookAt:
                    LookAtCommand lookAtCommand = new LookAtCommand();
                    serializer.Populate(jObject.CreateReader(), lookAtCommand);
                    return lookAtCommand;

                case ObjectCommand.Sfx:
                    SfxCommand sfxCommand = new SfxCommand();
                    serializer.Populate(jObject.CreateReader(), sfxCommand);
                    return sfxCommand;

                case ObjectCommand.Wait:
                    WaitCommand waitCommand = new WaitCommand();
                    serializer.Populate(jObject.CreateReader(), waitCommand);
                    return waitCommand;

                case ObjectCommand.MoveObject:
                    MoveObjectCommand moveObjectCommand = new MoveObjectCommand();
                    serializer.Populate(jObject.CreateReader(), moveObjectCommand);
                    return moveObjectCommand;

                case ObjectCommand.Fade:
                    FadeCommand fadeCommand = new FadeCommand();
                    serializer.Populate(jObject.CreateReader(), fadeCommand);
                    return fadeCommand;

                case ObjectCommand.MovePlayer:
                    MovePlayerCommand movePlayerCommand = new MovePlayerCommand();
                    serializer.Populate(jObject.CreateReader(), movePlayerCommand);
                    return movePlayerCommand;

                default:
                    Command target = new Command();
                    throw new NotImplementedException("CommandConverter");
                    return target;
            }
        }

        public override bool CanWrite {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }
    }
}
