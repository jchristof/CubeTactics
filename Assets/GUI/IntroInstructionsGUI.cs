using Assets;
using Assets.Extensions;
using Assets.Locale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    public class IntroInstructionsGUI : MonoBehaviour {
        Rect _guiBox;
        Rect _button;

        void Start(){
            Rect _fullScreenRect = new Rect(0, 0, Screen.width, Screen.height);

            _guiBox = new Rect(0, 0, 300, 100);
            _guiBox = _guiBox.CenterIn(_fullScreenRect);
            _button = new Rect(0, 150, 80, 20);
            _button = _button.CenterIn(_fullScreenRect);
        }

        void OnGUI() {
            string instructions = LocaleText.Text[CompositionRoot.Map.MapProperties.Description].Replace(@"\n", System.Environment.NewLine);
            GUI.Box(_guiBox, instructions);

            if (GUI.Button(_button, LocaleText.Text["ButtonOk"]) || Input.anyKey) {
                this.enabled = false;
                CompositionRoot.PlayerController.InputEnabled = true;
            }
            
        }
    }

