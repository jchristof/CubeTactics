using Assets.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class ScriptExecutor : IScriptExecutor {
        public ScriptExecutor(IMap map, IList<MapObject> mapObjects) {
            if (map == null)
                throw new ArgumentNullException("map");

            if (mapObjects == null)
                throw new ArgumentNullException("mapObjects");

            _map = map;
            _mapObjects = mapObjects;
        }

        readonly IMap _map;
        readonly IList<MapObject> _mapObjects;

        public void Execute(CommandList commandList) {
            if (commandList == null || commandList.Commands == null)
                return;

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
            }
        }

        void Destroy(Command cmd) {

        }
    }
}
