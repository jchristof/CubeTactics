
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ScriptBuilder.ScriptCommands.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ScriptBuilder.ScriptCommands.Positioning;
using Assets.Script;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ScriptBuilder {
    
    [JsonConverter(typeof(CommandConverter))]
    public class Command {
        public Command(){
            Identifier = new Identifier();
        }

        [Category("Game Object")]
        [ExpandableObject]
        public virtual Identifier Identifier { get; set; }

        [Browsable(false)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ObjectCommand ObjectCommand { get; set; }

        [Browsable(false)]
        public virtual object SecondaryInfo {
            get { return Identifier.ObjectName; }
        }

        public virtual void Execute() { }
    }
}
