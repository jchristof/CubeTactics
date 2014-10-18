using Newtonsoft.Json;
using ScriptBuilder.ScriptCommands.Commands;
using ScriptBuilder.ScriptCommands.Positioning;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ScriptBuilder.ScriptCommands {
    [JsonConverter(typeof(CommandConverter))]
    public class MoveObjectCommand : Command {

        public MoveObjectCommand() {
            Position = new Position();
            DestinationObject = new Identifier();
        }
        [Category("Destination")]
        [ExpandableObject]
        public Position Position { get; set; }
        [Category("Destination")]
        [ExpandableObject]
        public Identifier DestinationObject { get; set; }
    }
}
