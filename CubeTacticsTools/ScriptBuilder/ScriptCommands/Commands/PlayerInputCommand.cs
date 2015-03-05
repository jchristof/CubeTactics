using Assets;
using Assets.Script;
using Newtonsoft.Json;
using ScriptBuilder.ScriptCommands.Positioning;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBuilder.ScriptCommands.Commands {
    public class PlayerInputCommand : Command {

        public PlayerInputState PlayerInputState { get; set; }

        [Browsable(false)]
        public override Identifier Identifier { get; set; }

        public override object SecondaryInfo {
            get { return PlayerInputState; }
        }
    }
}
