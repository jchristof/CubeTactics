using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script {
    public class ScriptExecutor : IScriptExecutor {
        public ScriptExecutor() {
        }

        public void Execute(CommandList commandList) {
            if (commandList == null)
                return;

        }
    }
}
