using UnityEngine;
using System.Collections;
using System;
using Assets.Level;
using Assets;
using Assets.Game;

public class LevelMenuGUI : MonoBehaviour {

    void OnGUI() {
        int yPos = 0;

        foreach (var e in Enum.GetValues(typeof(LevelName))) {
            yPos += 50;
            if (GUI.Button(new Rect(20, yPos, 200, 30), e.ToString())) {
                Application.LoadLevel("scenedemo");
                Game.levelToLoad = (LevelName)e;
            }
        }
    }
}
