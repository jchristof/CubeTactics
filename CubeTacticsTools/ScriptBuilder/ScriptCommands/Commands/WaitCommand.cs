using Assets.Script;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace ScriptBuilder.ScriptCommands.Commands {
    [JsonConverter(typeof(CommandConverter))]
    public class WaitCommand : Command {
        public int Milliseconds { get; set; }

        [Browsable(false)]
        public override object SecondaryInfo {
            get { return Milliseconds; }
        }

        [Browsable(false)]
        public override string ObjectName { get; set; }

        [Browsable(false)]
        public override ObjectType ObjectType { get; set; }
    }
}
