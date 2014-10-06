using UnityEngine;
using System.Collections;
using System;
using Assets.Level;
using Assets;
using Assets.Game;

public class LevelMenuGUI : MonoBehaviour {

    GUIStyle myButtonStyle;
    FMOD.Studio.EventInstance guiSound;
    void Start() {
        
    }


    void OnGUI() {
        GUI.depth = 1000;
        int yPos = 0;
        myButtonStyle = new GUIStyle(GUI.skin.button);
        myButtonStyle.fontSize = 24;
        Font myFont = (Font)Resources.Load("Fonts/Mario-Kart-DS", typeof(Font));
        myButtonStyle.font = myFont;
        foreach (var e in Enum.GetValues(typeof(LevelName))) {
            yPos += 90;
            if (GUI.Button(new Rect(20, yPos, 500, 75), e.ToString(), myButtonStyle)) {
                guiSound = FMOD_StudioSystem.instance.GetEvent("event:/UI/Okay");
                guiSound.start();    

                SceneFadeInOut sceneFader = GameObject.Find("ScreenFader").GetComponent<SceneFadeInOut>();
                sceneFader.SceneEnding = true;
                enabled = false;
                sceneFader.OnLevelLoad = () => {
                    Application.LoadLevel("scenedemo");
                    Game.levelToLoad = (LevelName)e;
                };
                return;
            }
        }
        yPos += 90;
        if (GUI.Button(new Rect(20, yPos, 500, 75), "Quit", myButtonStyle)) {
            guiSound = FMOD_StudioSystem.instance.GetEvent("event:/UI/Cancel");
            guiSound.start();
            Application.Quit();
        }
    }

    void OnDestroy() {
        guiSound.release();
    }
}
