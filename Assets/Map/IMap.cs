using Assets.Script;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Map {
    public interface IMap {
        void ForeachObject(Action<MapObject> perObjectAction);
        void ForeachTile(Action<int, int, Tile> perTileAction, MapLayerName layerName);
        void ForeachTile(Action<int, int, int> perTileAction, MapLayerName layerName);
        MapLayer GetLayerByName(MapLayerName layerName);
        void LoadMap(string filename);
        PropertiesMap MapProperties { get; }
        IList<MapObject> MapObjects { get; set; }
        int PixelXToTileX(int x);
        int PixelYToTileY(int y);
        int Height { get; }
        int Width { get; }
        int FlatTileIndex(int x, int y);
        Vector3 FromFlatTileIndex(int index);
        bool PositionWithinMapBounds(UnityEngine.Vector3 position);
        void CreateTileAt(Vector3 position, MapLayerName layerName);
        void CreateTileAt(int x, int y, int tileIndex);
        void RemoveTileAt(int x, int y, MapLayerName layerName);
        void RemoveTileAt(UnityEngine.Vector3 position, MapLayerName layerName);
        Dictionary<string, IList<Command>> ScriptList { get; set; }
        UnityEngine.Vector3 SpawnPoint { get; set; }
        Tile TileAtMapPosition(int x, int y, MapLayerName layerName);
        Tile TileAtMapPosition(UnityEngine.Vector3 position, MapLayerName layerName);
        Tile TileAtTileSetIndex(int tileSetIndex);
        int TileIndexAt(int x, int y, MapLayerName layerName);
        int TileIndexAt(UnityEngine.Vector3 position, MapLayerName layerName);
        UnityEngine.Vector2[] UVForTileType(int tileIndex, UnityEngine.Mesh mesh);
    }
}
