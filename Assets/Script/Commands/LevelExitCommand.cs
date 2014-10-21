using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Commands {
    public class LevelExitCommand :Command {

        public override void Execute() {
            CompositionRoot.RunOnMainThread(() => {
                SceneFadeInOut sceneFader = GameObject.Find("ScreenFader").GetComponent<SceneFadeInOut>();
                sceneFader.SceneEnding = true;
                sceneFader.OnLevelLoad = () => {
                    Application.LoadLevel("loadmenu");
                };
                
            });
        }
    }
}
