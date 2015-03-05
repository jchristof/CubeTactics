using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Editor {

 
 public static class DebugLog {
 
 #if UNITY_EDITOR
     public static Dictionary<string,bool> tags = new Dictionary<string,bool>();
 #endif
     
     public static void Log(string tag, string message){
 #if UNITY_EDITOR
         if(!tags.ContainsKey(tag)){
             Debug.LogWarning("Tag "+ tag +" not found in MyDebugger's TagDictionary! Adding it...");
             tags.Add(tag,true);
         }
         if(tags[tag] == true)
             Debug.Log(message);
 #endif 
     }
     
     public static Dictionary<string,bool> IteratableDictionary(){
 #if UNITY_EDITOR
         return new Dictionary<string, bool>(tags);
 #endif
         return null;
     }
     
     public static void SetTag(string tag, bool flag){
 #if UNITY_EDITOR
         if(tags.ContainsKey(tag))
             tags[tag]= flag;
         else
             tags.Add(tag,flag);
 #endif
     }
     
 }
    
}
