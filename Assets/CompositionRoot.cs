
using Assets.Language;
using Assets.Locale;
using Assets.Map;
using Assets.Script;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Assets {
    public static class CompositionRoot {

        static Assets.Game.Game _game;
        public static Assets.Game.Game Game {
            get {
                if (_game == null) {
                    _game = GameObject.Find("GameManager").GetComponent<Assets.Game.Game>();
                    InitializeGame();
                }
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

        static AudioPlayer _audioPlayer;

        public static AudioPlayer AudioPlayer {
            get {
                if (_audioPlayer == null)
                    _audioPlayer = new AudioPlayer();

                return _audioPlayer;
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
                }
                return _scriptExecutor;
            }
        }

        public static void RunOnMainThread(Action action){
            _invoker.Add(action);
        }

        static Invoker _invoker;
        public static void InitializeGame(){
            _invoker = GameObject.Find("Invoker").GetComponent<Invoker>();
        }

        static readonly string _scriptPath = "Scripts/";
        static public Dictionary<string, IList<Command>> LoadScripts(string scriptName) {
            if(string.IsNullOrEmpty(scriptName))
                return default(Dictionary<string, IList<Command>>);

            string scriptResource = scriptName.Split('.')[0];
            string scriptJson = (Resources.Load(string.Format("{0}{1}", _scriptPath, scriptResource)) as TextAsset).text;

            try {
                return (Dictionary<string, IList<Command>>)JsonConvert.DeserializeObject(scriptJson, typeof(Dictionary<string, IList<Command>>));
            }
            catch (Exception e) {
                MonoBehaviour.print(e.Message);
            }

            return default(Dictionary<string, IList<Command>>);     
        }
    }
}
