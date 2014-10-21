using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBuilder.ScriptCommands.Commands {
    [JsonConverter(typeof(CommandConverter))]
    public class CameraZoomCommand : Command {
        [Browsable(true)]
        public int Height { get; set; }
        [Browsable(true)]
        public int Milliseconds { get; set; }
        [Browsable(true)]
        public override object SecondaryInfo {
            get { return "Camera"; }
        }
    }
}
