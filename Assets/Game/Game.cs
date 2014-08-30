using Assets.Game.Conditions;
using Assets.Level;
using Assets.Map;
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

        public static LevelName levelToLoad;

        public void Awake() {

            Assets.Map.Map _map = new Assets.Map.Map();

            string mapFileToLoad = levelToLoad.ToString();

            _map.LoadMap(mapFileToLoad);
            CompositionRoot.Map = _map;

            string levelConditionsClassName = CompositionRoot.Map.MapMeta.properties.Conditions;
            Type elementType = Type.GetType(
                string.Format("Assets.Game.Conditions.{0}, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null", levelConditionsClassName));

            _levelConditions = (LevelConditions)Activator.CreateInstance(elementType);
            CompositionRoot.Playfield.CreateTrail = CompositionRoot.Map.MapMeta.properties.Trail == "Solid";
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
