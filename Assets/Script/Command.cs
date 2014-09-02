using Assets.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script {
    public class Command {
        public MapLayer Layer { get; set; }
        public IList<int> Tile { get; set; }
        public ObjectCommand ObjectCommand { get; set; }

    }
}
