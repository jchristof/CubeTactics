using UnityEngine;
using System.Collections;
using System;
using Assets.Level;
using Assets;
using Assets.Game;

public class LevelMenuGUI : MonoBehaviour {

    GUIStyle myButtonStyle;
    void Start() {

    }

    void OnGUI() {
        int yPos = 0;
        myButtonStyle = new GUIStyle(GUI.skin.button);
        myButtonStyle.fontSize = 24;
        Font myFont = (Font)Resources.Load("Fonts/Mario-Kart-DS", typeof(Font));
        myButtonStyle.font = myFont;
        foreach (var e in Enum.GetValues(typeof(LevelName))) {
            yPos += 90;
            if (GUI.Button(new Rect(20, yPos, 500, 75), e.ToString(), myButtonStyle)) {
                Application.LoadLevel("scenedemo");
                Game.levelToLoad = (LevelName)e;
            }
        }
        yPos += 90;
        if (GUI.Button(new Rect(20, yPos, 500, 75), "Quit", myButtonStyle))
                Application.Quit();
    }
}
