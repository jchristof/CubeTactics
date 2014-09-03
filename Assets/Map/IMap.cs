using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Map {
    public interface IMap {
        int PixelXToTileX(int x);
        int PixelYToTileY(int y);
    }
}
