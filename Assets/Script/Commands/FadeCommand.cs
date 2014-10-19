using Assets.Actors;
using Assets.Script.Positioning;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Assets.Script.Commands {
    [JsonConverter(typeof(CommandConverter))]
    public class FadeCommand : Command {

        public Direction Direction { get; set; }
        public int Milliseconds { get; set; }

        readonly int FADE_STEPS = 20;
        int sleepTime;
        float fadeStep;
        int direction;
        float alpha;

        public override void Execute() {
            fadeStep = 1.0f/FADE_STEPS;
            direction = Direction == Direction.In ? 1 : -1;
            alpha = Direction == Direction.In ? 0.0f : 1.0f;
            sleepTime = Milliseconds / FADE_STEPS;

            for (int i = 0; i < FADE_STEPS; i++) {
                CompositionRoot.RunOnMainThread(() => {
                    IActor actor = Identifier.Actor;
                    actor.SetFade(alpha);
                });
                alpha += direction * fadeStep;
                Thread.Sleep(sleepTime);
            }
        }
    }
}
