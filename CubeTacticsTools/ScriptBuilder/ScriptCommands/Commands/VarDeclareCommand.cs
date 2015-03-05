using Assets.Script.Expressions;
using ScriptBuilder.ScriptCommands.Positioning;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBuilder.ScriptCommands.Commands {
    public class VarDeclareCommand : Command {
        public VarDeclareCommand() {
            Variable = new List<ScriptValueDecl>();
        }

        public List<ScriptValueDecl> Variable { set; get; }

        [Browsable(false)]
        public override Identifier Identifier { get; set; }

        [Browsable(false)]
        public override object SecondaryInfo {
            get { return null; }
        }
    }
}
