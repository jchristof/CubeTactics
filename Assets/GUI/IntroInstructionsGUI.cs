using Assets;
using Assets.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


    public class IntroInstructionsGUI : MonoBehaviour {
        static Rect _fullScreenRect = new Rect(0,0,Screen.width, Screen.height);
        Rect _guiBox;
        Rect _button;

        void Start(){
            _guiBox = new Rect(0, 0, 300, 100);
            _guiBox = _guiBox.CenterIn(FullScreenRect);
            _button = new Rect(0, 150, 80, 20);
            _button = _button.CenterIn(FullScreenRect);
        }
        static public Rect FullScreenRect {
            get { return _fullScreenRect; }
        }


        void OnGUI() {
            GUI.Box(_guiBox, CompositionRoot.Map.MapMeta.properties.Descriptions);
            if (GUI.Button(_button, "Ok") || Input.anyKey) {
                this.enabled = false;
                CompositionRoot.PlayerController.InputEnabled = true;
            }
            
        }
    }

