using Assets.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.Positioning {
    public class Indentifier {
        //lazy actor reference cache to eliminate multiple lookups
        IActor _actor;
        public IActor Actor {
            get {
                if (_actor == null)
                    _actor = this.ActorFor();

                return _actor;
            }
        }

        public string ObjectName { get; set; }
        public ObjectType ObjectType { get; set; }
    }
}
