using Assets.Game.Conditions;
using Assets.Level;
using Assets.Locale;
using Assets.Map;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Game {
    public class Game : MonoBehaviour{
        GameState _state;

        protected LevelConditions _levelConditions;
        Action _initializeAction;
        string _regionTextFile = "EN_US";
        string _musicRoot = "Music";

        public static LevelName levelToLoad;

        public void Awake() {
 
            TextAsset languageText = Resources.Load(_regionTextFile) as TextAsset;
            LocaleText.CreateText(languageText.text);

            CompositionRoot.Map = new Assets.Map.Map();

            string mapFileToLoad = levelToLoad.ToString();

            CompositionRoot.Map.LoadMap(mapFileToLoad);

            string levelConditionsClassName = CompositionRoot.Map.MapProperties.Conditions;
            Type elementType = Type.GetType(
                string.Format("Assets.Game.Conditions.{0}, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", levelConditionsClassName));

            _levelConditions = (LevelConditions)Activator.CreateInstance(elementType);
            CompositionRoot.Playfield.CreateTrail = CompositionRoot.Map.MapProperties.Trail == "Solid";

            if(!string.IsNullOrEmpty(CompositionRoot.Map.MapProperties.Music)){
                CompositionRoot.MusicPlayer.audio.clip = Resources.Load(string.Format("{0}/{1}", _musicRoot, CompositionRoot.Map.MapProperties.Music)) as AudioClip;
                CompositionRoot.MusicPlayer.audio.Play();
            }
        }

        GameState GameState { 
            get { return _state; } 
            set {
                if (_state == value)
                    return;
                GameStateChange(value);

                print(string.Format("GameState changed from {0} to {1}", _state.ToString(), value.ToString()));
                _state = value; 
                
            } 
        }

        public List<Condition> Conditions {
            get { return _levelConditions == null ? null : _levelConditions.Conditions; }
        }

        public bool LevelComplete {
            get { return _levelConditions.Complete; }
        }

        public bool LevelFailed {
            get {
                return _levelConditions.Failed;
            }
        }

        void Update() {
            _levelConditions.Update();
        }

        public void ExecutePlayerMove(Vector3 position) {
            _levelConditions.ExecutePlayerMove(position);
            _levelConditions.Evaluate();
            if (_levelConditions.Failed) {
                print("Level conditions failed");
            } 
            else if (_levelConditions.Complete) {
                print("Level complete");
            }
        }

        void GameStateChange(GameState newState) {
            if (newState == Assets.Game.GameState.Win) {
                CompositionRoot.PlayerController.InputEnabled = false;
            }
        }
    }
}
