using Assets.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Commands {
    public class CreateCommand : Command {
        public CreateCommand() { }
        public CreateCommand(IMap map) {
            if(map == null)
                throw new ArgumentNullException("map");

            _map = map;
        }

        readonly IMap _map;

        public override void Execute(){
            CompositionRoot.RunOnMainThread(() => {
                MapObject mapObject = _map.MapObjects.Where(x => x.Name == ObjectName).First();
                if (mapObject.Type == MapObjectType.Tile) {
                    int x = _map.PixelXToTileX(mapObject.X);
                    int y = _map.PixelYToTileY(mapObject.Y);
                    _map.CreateTileAt(x, y, mapObject.Properties.TileIndex);
                    CompositionRoot.Playfield.CreateTileVisualAt(x, y, mapObject.Properties.TileIndex);
                }
            });
        }
    }
}
