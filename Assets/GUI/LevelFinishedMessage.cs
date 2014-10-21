﻿using Assets;
using Assets.Extensions;
using Assets.Locale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GUI {
    public class LevelFinishedMessage : MonoBehaviour {        
        Rect _guiBox;
        Rect _button;

        public bool Finished { get; set; }
        void Start() {
            Rect _fullScreenRect = new Rect(0, 0, Screen.width, Screen.height);
            _guiBox = new Rect(0, 0, 300, 100);
            _guiBox = _guiBox.CenterIn(_fullScreenRect);
            _button = new Rect(0, 150, 80, 20);
            _button = _button.CenterIn(_fullScreenRect);
        }

        void OnGUI() {
            bool complete = CompositionRoot.Game.LevelComplete;

            UnityEngine.GUI.Box(_guiBox, complete ? LocaleText.Text["LevelComplete"] : LocaleText.Text["LevelFailed"]);
            if (UnityEngine.GUI.Button(_button, LocaleText.Text["ButtonOk"]) || Input.anyKey) {
                this.enabled = false;
                Finished = true;
            }

        }
    }
}
