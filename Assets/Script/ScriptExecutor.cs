using Assets.Camera;
using Assets.Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class ScriptExecutor : UnityEngine.MonoBehaviour, IScriptExecutor  {
        IMap _map;
        IList<MapObject> _mapObjects;

        public IMap Map {
            set { _map = value; }
        }

        public IList<MapObject> MapObjects {
            set { _mapObjects = value; }
        }

        public void Execute(CommandList commandList){
            if (commandList == null || commandList.Commands == null)
                return;
            StartCoroutine(ExecuteAsync(commandList));
        }

        public IEnumerator ExecuteAsync(CommandList commandList) {

            foreach (var cmd in commandList.Commands) {
                if (cmd.ObjectCommand == ObjectCommand.Destroy) {
                    MapObject mapObject = _mapObjects.Where(x=>x.name == cmd.ObjectName).First();
                    if(mapObject.type == MapObjectType.Tile){
                        int x = _map.PixelXToTileX(mapObject.x);
                        int y = _map.PixelYToTileY(mapObject.y);
                        _map.RemoveTileAt(x, y, MapLayerName.Board);
                        CompositionRoot.Playfield.RemoveTileVisualAt(new Vector3(x, 0, y));
                    }
                }
                else if (cmd.ObjectCommand == ObjectCommand.Create) {
                    MapObject mapObject = _mapObjects.Where(x => x.name == cmd.ObjectName).First();
                    if (mapObject.type == MapObjectType.Tile) {
                        int x = _map.PixelXToTileX(mapObject.x);
                        int y = _map.PixelYToTileY(mapObject.y);
                        _map.CreateTileAt(x, y, mapObject.properties.tileindex);
                        CompositionRoot.Playfield.CreateTileVisualAt(x, y, mapObject.properties.tileindex);
                    }
                }
                else if(cmd.ObjectCommand == ObjectCommand.LookAt){
                    MapObject mapObject = _mapObjects.Where(x => x.name == cmd.ObjectName).FirstOrDefault();
                    _mapObjects.ToList().ForEach(x => { print(x.name); });

                    if(mapObject != null){
                        //if (mapObject.type == MapObjectType.Tile) {
                            int x = _map.PixelXToTileX(mapObject.x);
                            int y = _map.PixelYToTileY(mapObject.y);

                            GameObject go = GameObject.Find(string.Format("{0}", _map.FlatTileIndex(x, y)));
                            UnityEngine.Camera.main.GetComponent<CameraScript>().Target = go.transform;
                        //}
                    }
                    else{
                        GameObject go = GameObject.Find(cmd.ObjectName);
                        UnityEngine.Camera.main.GetComponent<CameraScript>().Target = go.transform;
                        print(string.Format("Look at tile name {0}, flat index {1}", cmd.ObjectName, go.name));
                    }
                }
                else if (cmd.ObjectCommand == ObjectCommand.Disable) {
                    GameObject.Find(cmd.ObjectName).GetComponent<PlayerController>().enabled = false;
                }
                else if (cmd.ObjectCommand == ObjectCommand.Enable) {
                    GameObject.Find(cmd.ObjectName).GetComponent<PlayerController>().enabled = true;
                }
                else if (cmd.ObjectCommand == ObjectCommand.Wait) {
                    yield return new WaitForSeconds(2);
                }
            }
            yield return null;
        }

        void Destroy(Command cmd) {

        }
    }
}
