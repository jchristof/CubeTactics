using Assets.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script {
    public class ScriptExecutor : IScriptExecutor {
        public ScriptExecutor(IMap map) {
            if (map == null)
                throw new ArgumentNullException("map");

            _map = map;
        }
        readonly IMap _map;

        public void Execute(CommandList commandList) {
            if (commandList == null || commandList.Commands == null)
                return;

            foreach (var cmd in commandList.Commands) {
                if (cmd.ObjectCommand == ObjectCommand.Destroy) {
                    if (cmd.Tile != null) {
                        _map.RemoveTileAt(cmd.Tile[0], cmd.Tile[1], MapLayerName.Board);
                        CompositionRoot.Playfield.RemoveTileVisualAt(new Vector3(cmd.Tile[0], 0, cmd.Tile[1]));
                    }
                }
                else if (cmd.ObjectCommand == ObjectCommand.Create) {
                    if (cmd.Tile != null) {
                        _map.CreateTileAt(cmd.Tile[0], cmd.Tile[1], cmd.TileIndex);
                        CompositionRoot.Playfield.CreateTileVisualAt(cmd.Tile[0], cmd.Tile[1], cmd.TileIndex);
                    }
                }
            }
        }

        void Destroy(Command cmd) {

        }
    }
}
