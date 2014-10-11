using Assets.Map;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Assets.Script {
    public class Command {
        public string ObjectName { get; set; }
        [Browsable(false)]
        public ObjectCommand ObjectCommand { get; set; }
        public ObjectType ObjectType { get; set; }

        public virtual void Execute() { }
    }

    public class CommandList {
        public CommandList() {
            Commands = new List<Command>();
        }
        public IList<Command> Commands { get; set; }
    }

}
