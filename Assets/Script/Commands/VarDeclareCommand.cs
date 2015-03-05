using Assets.Editor;
using Assets.Script.Expressions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Commands {
    [JsonConverter(typeof(CommandConverter))]
    public class VarDeclareCommand : Command {
        public List<ScriptValueDecl> Variable { set; get; }

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
