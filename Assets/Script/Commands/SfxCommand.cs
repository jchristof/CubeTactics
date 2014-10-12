using Assets.Audio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Commands {
    [JsonConverter(typeof(CommandConverter))]
    public class SfxCommand : Command {
        public SfxCommand() { }
        public SfxType SfxType { get; set; }
        public float Volume { get; set; }
        public override void Execute() {
            CompositionRoot.RunOnMainThread(() => {
                CompositionRoot.AudioPlayer.PlaySfx(SfxType);
            });
        }
    }
}
