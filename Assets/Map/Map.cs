using Assets.Map.Triggers;
using Assets.Script;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Extensions;

namespace Assets.Map {
    public class Map : IMap {
        MapModel _mapModel;
        Dictionary<int, Tile> tilelist = new Dictionary<int, Tile>();
        Tile emptyTile = new Tile {
            index = -1
        };

        public void LoadMap(string filename){

            _mapModel = CompositionRoot.LoadMap(filename);

            //cache tile type for faster lookup
            _mapModel.tilesets[0].tileproperties.ToList().ForEach(x => {
                tilelist.Add(x.index, x);
            });

            _mapModel.Validate();

            ImageWidthInTiles = _mapModel.tilesets[0].imagewidth / _mapModel.tilesets[0].tilewidth;
            ImageHeightInTiles = _mapModel.tilesets[0].imageheight / _mapModel.tilesets[0].tileheight;

            TileWidth = _mapModel.tilesets[0].tilewidth;
            TileHeight = _mapModel.tilesets[0].tileheight;

            ReorderTiles(GetLayerByName(MapLayerName.Board));
            ReorderObjects(GetLayerByName(MapLayerName.Object));

            MapObjects = GetLayerByName(MapLayerName.Object).objects;

            ScriptList = CompositionRoot.LoadScripts(_mapModel.properties.Scripts);

            SpawnPoint = MapObjects.Where(x => x.Type == MapObjectType.SpawnPoint).Cast<SpawnPoint>().First().Position;
        }

        public Vector3 SpawnPoint { get; set; }
        public IList<MapObject> MapObjects { get; set; }
        public Dictionary<string, IList<Command>> ScriptList { get; set; }

        void ReorderTiles(MapLayer mapLayer) {
            int[] mapdata = mapLayer.data.ToArray();
            int[] reorderedData = new int[0];
            for (int y = _mapModel.height - 1; y >= 0 ; y--) {
                for (int x = 0; x < _mapModel.width; x += mapLayer.width) {
                    int flatIndex = (y * _mapModel.width) + x;                    
                    reorderedData = reorderedData.Concat( mapdata.SubArray(flatIndex, mapLayer.width)).ToArray();
                }
            }
            mapLayer.data = new List<int>(reorderedData);
        }

        void ReorderObjects(MapLayer mapLayer) {

           var list =  mapLayer.objects.Select(x => { x.Y = (_mapModel.height * _mapModel.tilewidth) - x.Y; return x; }).ToList();
           list.AsEnumerable();
        }

        int ImageWidthInTiles {get; set;}
        int ImageHeightInTiles { get; set;}

        int TileWidth { get; set; }
        int TileHeight { get; set; }

        public int PixelXToTileX(int x) {
            return x / TileWidth;
        }

        public int PixelYToTileY(int y) {
            return y / TileHeight;
        }

        public int TileXToPixelX(int x){
            return x*TileWidth;
        }

        public int TileYToPixelY(int y) {
            return y * TileHeight;
        }

        public int Height { get { return _mapModel.height; } }
        public int Width { get { return _mapModel.width; } }

        public PropertiesMap MapProperties {
            get {
                return _mapModel.properties;
            }
        }

        public int FlatTileIndex(int x, int y) {
            return (y * _mapModel.height) + x;
        }

        public Vector3 FromFlatTileIndex(int index) {
            return new Vector3((index % Width)*TileWidth, 0, (index / Width)*TileHeight);
        }

        public Vector3 FromTileIndex(Vector3 position) {

            return new Vector3(position.x * TileWidth, 0, (_mapModel.height - position.z) * TileHeight);
        }

        public Vector2[] UVForTileType(int tileIndex, Mesh mesh) {
            //mesh.uv[0] = 0.125f * xIndex);
            Vector2[] uvs = new Vector2[mesh.vertices.Length];
            int i = 0;
            int xIndex = tileIndex % ImageWidthInTiles;
            int yIndex = tileIndex / ImageHeightInTiles;
            while (i < uvs.Length) {
                uvs[i] = new Vector2(((mesh.vertices[i].x + .5f) * .125f) + (.125f * xIndex), ((mesh.vertices[i].y + .5f) * -.125f) + (-.125f * yIndex));
                i++;
            }

            Vector2 temp = uvs[0];
            uvs[0] = uvs[3];
            uvs[3] = temp;

            temp = uvs[1];
            uvs[1] = uvs[2];
            uvs[2] = temp;

            return uvs;
        }

        public IEnumerable<MapObject> MapObjectAt(Vector3 position) {
            return GetLayerByName(MapLayerName.Object).objects.
                Where(x => PixelXToTileX(x.X) == position.x).
                Where(x => PixelYToTileY(x.X) == position.y);
        }

        public Tile TileAtTileSetIndex(int tileSetIndex) {
            if (tileSetIndex == -1 || !tilelist.ContainsKey(tileSetIndex))
                return emptyTile;

            return tilelist[tileSetIndex];
        }

        public Tile TileAtMapPosition(Vector3 position, MapLayerName layerName) {
            return TileAtMapPosition(Convert.ToInt32(position.x), Convert.ToInt32(position.z), layerName);
        }

        public Tile TileAtMapPosition(int x, int y, MapLayerName layerName) {
            int flatIndex = (y * _mapModel.height) + x;
            MapLayer layer = GetLayerByName(layerName);
            int tileSetIndex = layer.data[flatIndex] - 1;

            if(tileSetIndex == -1)
                return emptyTile;

            return tilelist[tileSetIndex];
        }

        public void CreateTileAt(Vector3 position, MapLayerName layerName) {
            //CreateTileAt(Convert.ToInt32(position.x), Convert.ToInt32(position.z), layerName);
        }

        public void CreateTileAt(int x, int y, int tileIndex) {
            MapLayer layer = GetLayerByName(MapLayerName.Board);
            int flatIndex = (y * _mapModel.height) + x;
            layer.data[flatIndex] = tileIndex;
        }

        public void RemoveTileAt(Vector3 position, MapLayerName layerName) {
            RemoveTileAt(Convert.ToInt32(position.x), Convert.ToInt32(position.z), layerName);
        }

        public void RemoveTileAt(int x, int y, MapLayerName layerName) {
            MapLayer layer = GetLayerByName(layerName);
            int flatIndex = (y * _mapModel.height) + x;
            layer.data[flatIndex] = 0;
        }

        public int TileIndexAt(Vector3 position, MapLayerName layerName) {
            return TileIndexAt(Convert.ToInt32(position.x), Convert.ToInt32(position.z), layerName);
        }

        public int TileIndexAt(int x, int y, MapLayerName layerName) {
            MapLayer layer = GetLayerByName(layerName);
            int flatIndex = (y * _mapModel.height) + x;
            return layer.data[flatIndex] - 1;
        }

        public MapLayer GetLayerByName(MapLayerName layerName) {
            return _mapModel.layers.First(x => x.name == layerName.ToString());
        }

        public void ForeachObject(Action<MapObject> perObjectAction){
            MapLayer layer = GetLayerByName(MapLayerName.Object);
            foreach(var o in layer.objects)
                perObjectAction(o);
        }

        public void ForeachTile(Action<int, int, int> perTileAction, MapLayerName layerName) {
            MapLayer layer = GetLayerByName(layerName);

            for (int y = 0; y < _mapModel.height; y++) {
                for (int x = 0; x < _mapModel.width; x++) {
                    int flatIndex = (y * _mapModel.width) + x;                    
                    int tileSetIndex = layer.data[flatIndex] - 1;

                    perTileAction(x, y, tileSetIndex);
                }
            }
        }

        public void ForeachTile(Action<int, int, Tile> perTileAction, MapLayerName layerName) {
            MapLayer layer = GetLayerByName(layerName);

            for (int y = 0; y < _mapModel.height; y++) {
                for (int x = 0; x < _mapModel.width; x++) {

                    Tile t = TileAtMapPosition(x, y, layerName);
                    perTileAction(x, y, t);
                }
            }
        }

        public bool PositionWithinMapBounds(Vector3 position) {
            int x = Convert.ToInt32(position.x);
            int y = Convert.ToInt32(position.z);
            if (x < 0 || y < 0)
                return false;

            if (x >= _mapModel.width || y >= _mapModel.height)
                return false;

            return true;
        }
    }
}
