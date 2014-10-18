using Assets.Map;
using Assets.Script.Commands;
using Assets.Script.Positioning;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Assets.Script {
    [JsonConverter(typeof(CommandConverter))]
    public class Command {

        public Indentifier Identifier { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ObjectCommand ObjectCommand { get; set; }

        public virtual void Execute() { }
    }
}
