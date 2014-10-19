using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Commands {
    public class PlayerInputCommand : Command {
        public PlayerInputState PlayerInputState { get; set; }

        public override void Execute() {
            CompositionRoot.RunOnMainThread(() => {
                GameObject.Find("Player").GetComponent<PlayerController>().InputEnabled =
                    PlayerInputState == Script.PlayerInputState.MoveEnabled ? true : false;
            });
        }
    }
}
