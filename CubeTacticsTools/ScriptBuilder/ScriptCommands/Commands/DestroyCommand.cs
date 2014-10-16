using Newtonsoft.Json;
using ScriptBuilder.ScriptCommands.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptBuilder.ScriptCommands.Commands  {
    [JsonConverter(typeof(CommandConverter))]
    public class DestroyCommand : Command {
        public DestroyCommand() { }
    }
}
