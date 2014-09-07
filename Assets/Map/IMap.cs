using System;
using UnityEngine;
namespace Assets.Map {
    public interface IMap {
        void ForeachObject(Action<MapObject> perObjectAction);
        void ForeachTile(Action<int, int, Tile> perTileAction, MapLayerName layerName);
        void ForeachTile(Action<int, int, int> perTileAction, MapLayerName layerName);
        MapLayer GetLayerByName(MapLayerName layerName);
        void LoadMap(string filename);
        PropertiesMap MapProperties { get; }
        int PixelXToTileX(int x);
        int PixelYToTileY(int y);
        int Height { get; }
        int Width { get; }
        bool PositionWithinMapBounds(UnityEngine.Vector3 position);
        void CreateTileAt(Vector3 position, MapLayerName layerName);
        void CreateTileAt(int x, int y, int tileIndex);
        void RemoveTileAt(int x, int y, MapLayerName layerName);
        void RemoveTileAt(UnityEngine.Vector3 position, MapLayerName layerName);
        System.Collections.Generic.IEnumerable<Assets.Map.Script.Script> Scripts { get; set; }
        UnityEngine.Vector3 SpawnPoint { get; set; }
        Tile TileAtMapPosition(int x, int y, MapLayerName layerName);
        Tile TileAtMapPosition(UnityEngine.Vector3 position, MapLayerName layerName);
        Tile TileAtTileSetIndex(int tileSetIndex);
        int TileIndexAt(int x, int y, MapLayerName layerName);
        int TileIndexAt(UnityEngine.Vector3 position, MapLayerName layerName);
        System.Collections.Generic.List<Trigger> Triggers { get; set; }
        UnityEngine.Vector2[] UVForTileType(int tileIndex, UnityEngine.Mesh mesh);
    }
}
