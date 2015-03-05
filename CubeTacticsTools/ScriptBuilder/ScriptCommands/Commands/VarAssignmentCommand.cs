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
    public class VarAssignmentCommand : Command{
        public string Target { set; get; }
        public string Expression { set; get; }


        [Browsable(false)]
        public override Identifier Identifier { get; set; }

        [Browsable(false)]
        public override object SecondaryInfo {
            get { return null; }
        }

    }
}
