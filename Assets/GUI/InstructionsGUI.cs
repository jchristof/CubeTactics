using UnityEngine;
using System.Collections;
using Assets;

public class InstructionsGUI : MonoBehaviour {

    void OnGUI() {

        int yPos = 10;
        GUI.Box(new Rect(10, yPos, 300, 30), CompositionRoot.Map.MapMeta.properties.Name);
        yPos += 30;

        foreach(var c in CompositionRoot.Game.Conditions){
            GUI.Toggle(new Rect(10, yPos, 300, 30), c.Complete || c.Failed, (c.Failed ? "X" : "") + c.Instructions);
            
            yPos += 30;
        }

        yPos += 30;
        if (CompositionRoot.Game.LevelComplete) {
            GUI.Box(new Rect(10, yPos, 300, 30), "Level Complete");
            
        }
        else if (CompositionRoot.Game.LevelFailed) {
            GUI.Box(new Rect(10, yPos, 300, 30), "Level Fail");
        }

        yPos += 30;
        if (GUI.Button(new Rect(20, yPos, 110, 20), "Reload Level")) {
            Application.LoadLevel("scenedemo");
        }

        yPos += 30;
        if (GUI.Button(new Rect(20, yPos, 110, 20), "Quit")) {
            Application.Quit();
        }
    }
}