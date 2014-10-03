using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script {
    public interface IScriptExecutor {
        void Execute(CommandList commandList);
        IEnumerator ExecuteAsync(CommandList commandList);
    }
}
