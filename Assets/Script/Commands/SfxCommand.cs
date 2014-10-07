using Assets.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Commands {
    class SfxCommand : Command {
        public SfxType SfxType { get; set; }
        public override void Execute() {
            CompositionRoot.RunOnMainThread(() => {
                CompositionRoot.AudioPlayer.PlaySfx(SfxType);
            });
        }
    }
}
