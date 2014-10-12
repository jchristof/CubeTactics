using Assets.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Extensions {
    static public class MapModelExtensions {
        static public void Validate(this MapModel mapModel){
            if (mapModel.tilesets[0].imagewidth % mapModel.tilesets[0].tilewidth != 0)
                throw new InvalidOperationException(string.Format("Tile image width {0} not an even multiple of tile width {1}",
                    mapModel.tilesets[0].imagewidth,
                    mapModel.tilesets[0].tilewidth));

            if (mapModel.tilesets[0].imageheight % mapModel.tilesets[0].tileheight != 0)
                throw new InvalidOperationException(string.Format("Tile image height {0} not an even multiple of tile height {1}",
                    mapModel.tilesets[0].imageheight,
                    mapModel.tilesets[0].tileheight));
        }
    }
}
