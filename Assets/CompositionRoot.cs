
using Assets.Locale;
using Assets.Map;
using Assets.Script;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets {
    public static class CompositionRoot {

        static Assets.Game.Game _game;
        public static Assets.Game.Game Game {
            get {
                if (_game == null)
                    _game = GameObject.Find("GameManager").GetComponent<Assets.Game.Game>();
                return _game;
            }
        }

        static AudioSource _musicPlayer;
        public static AudioSource MusicPlayer {
            get {
                if (_musicPlayer == null)
                    _musicPlayer = GameObject.Find("Sound").GetComponent<AudioSource>();
                return _musicPlayer;
            }
        }
                    
        static PlayerController _playerController;
        public static PlayerController PlayerController {
            get {
                if (_playerController == null)
                    _playerController = PlayerObject.GetComponent<PlayerController>();
                return _playerController;
            }
        }

        static GameObject _playerObject;
        public static GameObject PlayerObject {
            get {
                if (_playerObject == null)
                    _playerObject = GameObject.Find("Player");
                return _playerObject;
            }
        }

        static IMap _map;
        public static IMap Map {
            get { return _map; }
            set { _map = value; }
        }

        static PlayfieldScript _playField;
        public static PlayfieldScript Playfield {
            get {
                if (_playField == null)
                    _playField = GameObject.Find("Playfield").GetComponent<PlayfieldScript>();
                return _playField;
            }
        }

        static ScriptExecutor _scriptExecutor;
        public static ScriptExecutor ScriptExecutor {
            get {
                if (_scriptExecutor == null) {
                    _scriptExecutor = GameObject.Find("GameManager").GetComponent<ScriptExecutor>();
                    _scriptExecutor.Map = Map;
                    _scriptExecutor.MapObjects = Map.MapObjects;
                }
                return _scriptExecutor;
            }
        }

        public static void InitializeGame(){
        }
    }
}
