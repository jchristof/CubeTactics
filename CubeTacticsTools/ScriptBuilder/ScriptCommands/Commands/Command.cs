
using Assets.Script;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ScriptBuilder.ScriptCommands.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ScriptBuilder {
    [JsonConverter(typeof(CommandConverter))]
    public class Command {

        public virtual string ObjectName { get; set; }
        public virtual ObjectType ObjectType { get; set; }

        #region Serialization and Property Grid Properties
        [Browsable(false)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ObjectCommand ObjectCommand { get; set; }

        [Browsable(false)]
        public virtual object SecondaryInfo {
            get { return ObjectName; }
        }
        #endregion
        public virtual void Execute() { }
    }
}
