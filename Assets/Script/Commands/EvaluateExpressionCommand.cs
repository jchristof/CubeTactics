using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Commands {
    [JsonConverter(typeof(CommandConverter))]
    public class EvaluateExpressionCommand : Command {

        public string False { get; set; }
        public string True { get; set; }
        public string Expression { get; set; }
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
                if (CompositionRoot.ScriptExpression.Evaluate(Expression))
                    CompositionRoot.ScriptExecutor.Execute(True);
                else
                    CompositionRoot.ScriptExecutor.Execute(False);
            });
        }
    }
}
