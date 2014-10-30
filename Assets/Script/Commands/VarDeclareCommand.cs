using Assets.Editor;
using Assets.Script.Expressions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Assets.Script.Commands {
    [JsonConverter(typeof(CommandConverter))]
    public class VarDeclareCommand : Command {
#if OBJECTEDITOR 
        public VarDeclareCommand() {
            Variable = new List<ScriptValueDecl>();
        }
#endif
        public List<ScriptValueDecl> Variable { set; get; }
#if OBJECTEDITOR 
        [Browsable(false)]
        public override Identifier Identifier { get; set; }

        [Browsable(false)]
        public override object SecondaryInfo {
            get { return null; }
        }
#endif
        public override void Execute() {
            CompositionRoot.RunOnMainThread(() => {
                foreach (ScriptValueDecl variable in Variable) {
                    CompositionRoot.ScriptExpression.AddVariable(variable.Name, variable.Value);
                    DebugLog.Log("Expressions", "New variable: " + variable.Name + " value: " + variable.Value);
                }
            });
        }
    }
}
