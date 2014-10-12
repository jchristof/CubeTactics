using Assets.Map;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Commands {
    [JsonConverter(typeof(CommandConverter))]
    public class ActivateCommand : Command {
        public ActivateCommand() { }
        public ActivateCommand(IMap map) {
            if(map == null)
                throw new ArgumentNullException("map");

            _map = map;
        }

        readonly IMap _map;

        public override void Execute(){
            CompositionRoot.RunOnMainThread(() => {
                MapObject mapObject = _map.MapObjects.Where(x => x.Name == ObjectName).FirstOrDefault();
                if (mapObject != null)
                    mapObject.OnActivate();
            });
        }
    }
}
