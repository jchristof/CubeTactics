using Assets.Script;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBuilder.ScriptCommands.Commands {
    [JsonConverter(typeof(CommandConverter))]
    public class FadeCommand : Command {

        [Category("Parameters")]
        public Direction Direction { get; set; }
        [Category("Parameters")]
        public int Milliseconds { get; set; }
    }
}
