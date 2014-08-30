using Assets.Game;
using Assets.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    _playerObject = GameObject.Find("Cube");
                return _playerObject;
            }
        }

        static Assets.Map.Map _map;
        public static Assets.Map.Map Map {
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

        public static void InitializeGame(){
        }
    }
}
