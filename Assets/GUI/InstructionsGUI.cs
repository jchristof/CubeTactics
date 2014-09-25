﻿using UnityEngine;
using System.Collections;
using Assets;
using System.Linq;
using Assets.Locale;
using System.Collections.Generic;

public class InstructionsGUI : MonoBehaviour {

    void Awake() {
        _uiRect = new Rect();
    }

    Rect _uiRect;

    Rect Rect(int x, int y, int width, int height) {
        _uiRect.x = x;
        _uiRect.y = y;
        _uiRect.width = width;
        _uiRect.height = height;

        return _uiRect;
    }

    void OnGUI() {
        int yPos = 10;
        string levelName = CompositionRoot.Map.MapProperties.Name;
        GUI.Box(Rect(10, yPos, 300, 30), LocaleText.Text[levelName]);


        foreach(var c in CompositionRoot.Game.Conditions){
            yPos += 30;

            GUI.Toggle(Rect(10, yPos, 300, 30), c.Complete || c.Failed, (c.Failed ? "X " : "") + c.Instructions);
        }

        yPos += 30;

        if (CompositionRoot.Game.LevelComplete) {
            GUI.Box(Rect(10, yPos, 300, 30), LocaleText.Text["LevelComplete"]);

        }
        else if (CompositionRoot.Game.LevelFailed) {
            GUI.Box(Rect(10, yPos, 300, 30), LocaleText.Text["LevelFailed"]);

        }

        yPos += 30;
        if (GUI.Button(Rect(10, yPos, 300, 30), LocaleText.Text["ButtonReloadLevel"])) {
            Application.LoadLevel("scenedemo");
        }

        yPos += 30;
        if (GUI.Button(Rect(10, yPos, 300, 30), LocaleText.Text["ButtonQuit"])) {
            Application.LoadLevel("loadmenu");
        }
    }
}