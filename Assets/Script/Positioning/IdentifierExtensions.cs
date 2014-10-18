using Assets.Actors;
using Assets.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Positioning {
    static public class IdentifierExtensions {
        static public Vector3 PositionOf(this Indentifier self, IMap map) {

            switch (self.ObjectType) {
                case ObjectType.MapObject:
                    return map.MapObjects.Where(x => x.Name == self.ObjectName).First().Position;
                case ObjectType.GameObject:
                    return GameObject.Find(self.ObjectName).transform.position;
                case ObjectType.Tile:
                    return map.FromTileIndex(GameObject.Find(self.ObjectName).transform.position);
            }

            return Vector3.zero;
        }

        static public IActor ActorFor(this Indentifier self) {
            switch (self.ObjectType) {
                case ObjectType.MapObject:
                    throw new NotImplementedException();
                case ObjectType.GameObject:
                    return new GameObjectActor(GameObject.Find(self.ObjectName));
                case ObjectType.Tile:
                    throw new NotImplementedException();
            }
            return null;
        }
    }
}
