using Assets.Map;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script {
    public class Command {
        public string ObjectName { get; set; }
        public MapLayerName Layer { get; set; }
        public IList<int> Tile { get; set; }
        public int TileIndex { get; set; }
        public ObjectCommand ObjectCommand { get; set; }
    }

    public class CommandList {
        public IList<Command> Commands { get; set; }
    }

}
