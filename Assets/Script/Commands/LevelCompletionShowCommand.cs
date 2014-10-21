using Assets.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Assets.Script.Commands {
    public class LevelCompletionShowCommand : Command {

        public override void Execute() {
            LevelFinishedMessage levelFinisedMessage = null;
            CompositionRoot.RunOnMainThread(() => {
                levelFinisedMessage = CompositionRoot.Game.GetComponent<LevelFinishedMessage>();
                levelFinisedMessage.Finished = false;
                levelFinisedMessage.enabled = true;
            });

            bool finished = false;
            while (!finished) {
                CompositionRoot.RunOnMainThread(() => {
                    finished = levelFinisedMessage.Finished;
                });

                Thread.Sleep(1);
            }
        }
    }
}
