using Assets.Camera;
using Assets.Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Assets.Script {
    public class ScriptExecutor : UnityEngine.MonoBehaviour, IScriptExecutor  {
        IMap _map;

        public IMap Map {
            set { _map = value; }
        }

        public void Execute(IList<Command> commandList){
            if (commandList == null)
                return;
            //StartCoroutine(ExecuteAsync(commandList));

            var t = new Thread(() => {
                commandList.ToList().ForEach(x => x.Execute());
            });
            t.Start();
        }

        //public void ExecuteAsync(CommandList commandList) {
        //}

        public IEnumerator ExecuteAsync(IList<Command> commandList) {

            foreach (var cmd in commandList) {
                if (cmd.ObjectCommand == ObjectCommand.Destroy) {
                    MapObject mapObject = _map.MapObjects.Where(x => x.Name == cmd.ObjectName).First();
                    if (mapObject.Type == MapObjectType.Tile) {
                        int x = _map.PixelXToTileX(mapObject.X);
                        int y = _map.PixelYToTileY(mapObject.Y);
                        _map.RemoveTileAt(x, y, MapLayerName.Board);
                        CompositionRoot.Playfield.RemoveTileVisualAt(new Vector3(x, 0, y));
                    }
                }
                else if (cmd.ObjectCommand == ObjectCommand.Create) {
                    MapObject mapObject = _map.MapObjects.Where(x => x.Name == cmd.ObjectName).First();
                    if (mapObject.Type == MapObjectType.Tile) {
                        int x = _map.PixelXToTileX(mapObject.X);
                        int y = _map.PixelYToTileY(mapObject.Y);
                        _map.CreateTileAt(x, y, mapObject.Properties.TileIndex);
                        CompositionRoot.Playfield.CreateTileVisualAt(x, y, mapObject.Properties.TileIndex);
                    }
                }
                else if (cmd.ObjectCommand == ObjectCommand.LookAt) {
                    MapObject mapObject = _map.MapObjects.Where(x => x.Name == cmd.ObjectName).FirstOrDefault();
                    _map.MapObjects.ToList().ForEach(x => { print(x.Name); });

                    if (mapObject != null) {
                        //if (mapObject.type == MapObjectType.Tile) {
                        int x = _map.PixelXToTileX(mapObject.X);
                        int y = _map.PixelYToTileY(mapObject.Y);

                        GameObject go = GameObject.Find(string.Format("{0}", _map.FlatTileIndex(x, y)));
                        UnityEngine.Camera.main.GetComponent<CameraScript>().Target = go.transform;
                        //}
                    }
                    else {
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
                else if (cmd.ObjectCommand == ObjectCommand.Activate) {
                    MapObject mapObject = _map.MapObjects.Where(x => x.Name == cmd.ObjectName).FirstOrDefault();
                    if (mapObject != null)
                        mapObject.OnActivate();
                }
            }
            yield return null;
        }
    }
}
