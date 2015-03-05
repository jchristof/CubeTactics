using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Commands {
    [JsonConverter(typeof(CommandConverter))]
    public class VarAssignmentCommand : Command {

        public string Target { set; get; }
        public string Expression { set; get; }

        public override void Execute() {
            CompositionRoot.RunOnMainThread(() => {
                CompositionRoot.ScriptExpression.Assignment(Target, Expression);
            });
        }
    }
}
