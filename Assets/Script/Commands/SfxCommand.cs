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
                Position = (Position == null ? new Position() : Position); 
                Vector3 position = Position.Vector;
                if (ObjectType == Script.ObjectType.GameObject)
                    position = GameObject.Find(ObjectName).transform.position + position;
                else if (ObjectType == Script.ObjectType.MapObject)
                    position = CompositionRoot.Map.MapObjects.Where(x => x.Name == ObjectName).First().Position + position;
                else if (ObjectType == Script.ObjectType.Tile)
                    position = GameObject.Find(ObjectName).transform.position + position;

                CompositionRoot.AudioPlayer.PlaySfx(SfxType, position);
            });
        }
    }
}
