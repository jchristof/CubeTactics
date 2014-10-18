using Assets.Audio;
using ScriptBuilder.PropertyGridEditors;
using ScriptBuilder.ScriptCommands.Positioning;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ScriptBuilder.ScriptCommands.Commands {
    public class SfxCommand : Command{
        public SfxCommand() {
            Position = new Position();
        }
        //[Editor(typeof(PositionEditor), typeof(PositionEditor))]

        [Category("Audio")]
        public SfxType SfxType { get; set; }
        [Category("Audio")]
        public float Volume { get; set; }

        [ExpandableObject]
        [Category("Game Object")]
        public Position Position { get; set; }

        [Browsable(false)]
        public override object SecondaryInfo {
            get { return SfxType; }
        }
    }
}
