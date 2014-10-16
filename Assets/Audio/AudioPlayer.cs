using Assets.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets {
    public class AudioPlayer {
        public void PlayStepSound() {
            FMOD.Studio.EventInstance moveSound;
            moveSound = FMOD_StudioSystem.instance.GetEvent("event:/Character/Footsteps/Footsteps");
            moveSound.setParameterValue("Surface", 3f);

            moveSound.start();
            moveSound.setParameterValue("Surface", 0f);
        }

        public void PlayTeleportSound() {
            FMOD.Studio.EventInstance teleportSound;
            teleportSound = FMOD_StudioSystem.instance.GetEvent("event:/UI/Okay");
            teleportSound.start();
            teleportSound.release();
        }

        public void PlayTriggerSound() {
            FMOD.Studio.EventInstance teleportSound;
            teleportSound = FMOD_StudioSystem.instance.GetEvent("event:/Character/Hand Foley/Lightswitch");
            teleportSound.start();
            teleportSound.release();
        }

        public void PlaySfx(SfxType type, Vector3 position) {
            switch(type){
                case SfxType.PressurePlateClick:
                    PlayTriggerSound();
                    break;
            }
        }
    }
}
