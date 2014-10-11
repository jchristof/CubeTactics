using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Assets.Script.Commands {
    public class WaitCommand : Command {
        public int Milliseconds { get; set; }
        public override void Execute() {
            Thread.Sleep(Milliseconds);
        }
    }
}
