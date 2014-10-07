using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Assets.Script.Commands {
    public class WaitCommand : Command {
        public override void Execute() {
            Thread.Sleep(2000);
        }
    }
}
