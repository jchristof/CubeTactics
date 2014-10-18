using Assets.Audio;
using Assets.Script.Positioning;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Commands {
    [JsonConverter(typeof(CommandConverter))]
    public class SfxCommand : Command {
        public SfxCommand() { 
        }

        public SfxType SfxType { get; set; }
        public float Volume { get; set; }
        public virtual Position Position { get; set; }

        public override void Execute() {
            CompositionRoot.RunOnMainThread(() => {

                Vector3 position = Position == null ? Vector3.zero : Position.Vector;
                if (Identifier.ObjectType == Script.ObjectType.GameObject)
                    position = GameObject.Find(Identifier.ObjectName).transform.position + position;
                else if (Identifier.ObjectType == Script.ObjectType.MapObject)
                    position = CompositionRoot.Map.MapObjects.Where(x => x.Name == Identifier.ObjectName).First().Position + position;
                else if (Identifier.ObjectType == Script.ObjectType.Tile)
                    position = GameObject.Find(Identifier.ObjectName).transform.position + position;

                CompositionRoot.AudioPlayer.PlaySfx(SfxType, position);
            });
        }
    }
}
