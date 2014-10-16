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
        //[Editor(typeof(PositionEditor), typeof(PositionEditor))]

        public SfxType SfxType { get; set; }
        public float Volume { get; set; }

        [ExpandableObject]
        public Position Position { get; set; }

        [Browsable(false)]
        public override object SecondaryInfo {
            get { return SfxType; }
        }
    }
}
