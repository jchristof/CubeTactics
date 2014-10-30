using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor {
    public class DebuggerEditor : EditorWindow {
         enum bools{
         False,
         True
     }
     
     string newTag = "";
     bools newBools = bools.False;
     bool newValue = false;
     
     [MenuItem("DebugLog/log")]
     public static void Init(){
         GetWindow<DebuggerEditor>();
     }
     
     void OnEnable(){
         DebugLog.tags.Add("Scripts",true);
         DebugLog.tags.Add("Expressions",true);
     }
     
     void OnGUI(){
         foreach(KeyValuePair<string,bool> kvp in DebugLog.IteratableDictionary()){
             GUILayout.BeginHorizontal();
             EditorGUI.BeginChangeCheck();
             newValue = GUILayout.Toggle(DebugLog.tags[kvp.Key], kvp.Key);
             if(EditorGUI.EndChangeCheck()){
                 DebugLog.SetTag(kvp.Key,newValue);
             }
             GUILayout.EndHorizontal();
         }
         
         GUILayout.BeginHorizontal();
         newTag = GUILayout.TextField(newTag);
         newBools = (bools)EditorGUILayout.EnumPopup(newBools);
         GUILayout.EndHorizontal();
         if(GUILayout.Button("Add"))
             DebugLog.SetTag(newTag,bool.Parse(newValue.ToString()));
     }
 }
}
