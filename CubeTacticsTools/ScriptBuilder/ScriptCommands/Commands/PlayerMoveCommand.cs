
using ScriptBuilder.ScriptCommands.Positioning;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ScriptBuilder.ScriptCommands.Commands {
    public class PlayerMoveCommand : Command {
        public PlayerMoveCommand() {
            Position = new Position();
            DestinationObject = new Identifier();
        }

        [Browsable(false)]
        public override Identifier Identifier { get; set; }

        [Category("Destination")]
        [ExpandableObject]
        public Position Position { get; set; }
        [Category("Destination")]
        [ExpandableObject]
        public Identifier DestinationObject { get; set; }

        [Browsable(false)]
        public override object SecondaryInfo {
            get { return "Player"; }
        }
    
    }
}
