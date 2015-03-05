using Newtonsoft.Json;
using ScriptBuilder.ScriptCommands.Positioning;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBuilder.ScriptCommands.Commands {
    [JsonConverter(typeof(CommandConverter))]
    public class EvaluateExpressionCommand : Command {
        public string False { get; set; }
        public string True { get; set; }
        public string Expression { get; set; }

        [Browsable(false)]
        public override Identifier Identifier { get; set; }

        [Browsable(false)]

        public override object SecondaryInfo {
            get { return null; }
        }

    }
}
