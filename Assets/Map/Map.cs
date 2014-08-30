using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Map {
    public class Map {
        MapModel _mapModel;
        Dictionary<int, Tile> tilelist = new Dictionary<int, Tile>();
        Tile emptyTile = new Tile {
            index = -1
        };

        public void LoadMap(string filename){
            TextAsset temp = Resources.Load(filename) as TextAsset;
            _mapModel = JsonConvert.DeserializeObject<MapModel>(temp.text);            

            if (_mapModel.tilesets[0].tileproperties != null) {
                var dynObj = JObject.Parse((_mapModel.tilesets[0].tileproperties.ToString()));
           
                foreach (var v in dynObj) {
                    Tile t = JsonConvert.DeserializeObject<Tile>(v.Value.ToString());
                    t.index = Convert.ToInt32(v.Key);
                    tilelist.Add(t.index, t);
                }
            }

            if (_mapModel.tilesets[0].imagewidth % _mapModel.tilesets[0].tilewidth != 0)
                throw new InvalidOperationException(string.Format("Tile image width {0} not an even multiple of tile width {1}",
                    _mapModel.tilesets[0].imagewidth,
                    _mapModel.tilesets[0].tilewidth));

            if (_mapModel.tilesets[0].imageheight % _mapModel.tilesets[0].tileheight != 0)
                throw new InvalidOperationException(string.Format("Tile image height {0} not an even multiple of tile height {1}",
                    _mapModel.tilesets[0].imageheight,
                    _mapModel.tilesets[0].tileheight));

            _imageTileWidth = _mapModel.tilesets[0].imagewidth / _mapModel.tilesets[0].tilewidth;
            _imageTileHeight = _mapModel.tilesets[0].imageheight / _mapModel.tilesets[0].tileheight;
        }

        int _imageTileWidth;
        int ImageTileWidth {
            get { return _imageTileWidth; }
        }

        int _imageTileHeight;
        int ImageTileHeight {
            get { return _imageTileHeight; }
        }

        public MapLayerObject MapMeta {
            get {
                return _mapModel.layers[2].objects[0];
            }
        }

        public Vector2[] UVForTileType(int tileIndex, Mesh mesh) {

            Vector2[] uvs = new Vector2[mesh.vertices.Length];
            int i = 0;
            int xIndex = tileIndex % ImageTileWidth;
            int yIndex = tileIndex / ImageTileHeight;
            while (i < uvs.Length) {
                uvs[i] = new Vector2(((mesh.vertices[i].x + .5f) * .125f) + (.125f * xIndex), ((mesh.vertices[i].y + .5f) * -.125f) + (-.125f * yIndex));
                i++;
            }
            return uvs;
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

        public int TileIndexAt(Vector3 position, MapLayerName layerName) {
            return TileIndexAt(Convert.ToInt32(position.x), Convert.ToInt32(position.z), layerName);
        }

        public int TileIndexAt(int x, int y, MapLayerName layerName) {
            MapLayer layer = GetLayerByName(layerName);
            int flatIndex = (y * _mapModel.height) + x;
            return layer.data[flatIndex] - 1;
        }

        MapLayer GetLayerByName(MapLayerName layerName) {
            return _mapModel.layers.First(x => x.name == layerName.ToString());
        }

        public void ForeachTile(Action<int, int, int> perTileAction, MapLayerName layerName) {
            for (int y = 0; y < _mapModel.height; y++) {
                for (int x = 0; x < _mapModel.width; x++) {
                    int flatIndex = (y * _mapModel.width) + x;
                    MapLayer layer = GetLayerByName(layerName);
                    int tileSetIndex = layer.data[flatIndex] - 1;

                    perTileAction(x, y, tileSetIndex);
                }
            }
        }

        public bool PositionWithinMapBounds(Vector3 position) {
            if (position.x < 0 || position.z < 0)
                return false;

            if (position.x > _mapModel.width || position.z > _mapModel.height)
                return false;

            return true;
        }
    }
}
