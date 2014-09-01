using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Language;
using Assets.Map;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System;
using Assets;

public class PlayfieldScript : MonoBehaviour {

    public bool _crossEmptyTile;
    public bool _crossMyPath;
    public bool CreateTrail { get; set; }

    Vector3 _spawnPoint;
    List<Trigger> _triggers = new List<Trigger>();
    List<Tuple<Vector3, GameObject>> playfiedGrid = new List<Tuple<Vector3, GameObject>>();
    Map _map;

	// Use this for initialization
	void Start () {
        ClampToRectBounds = true;
        _map = CompositionRoot.Map;

        _map.ForeachTile(new Action<int, int, int>(PerTileMapSetup), MapLayerName.Board);
        //_map.ForeachTile(new Action<int, int, int>(PerTileObjectSetup), MapLayerName.Object);
        _map.ForeachObject(new Action<MapLayerObject> (PerObjectSetup));

        CompositionRoot.PlayerController.SpawnAt(_spawnPoint);
	}

     Vector3 PlayerPositionFromXY(int x, int y) {
        return new Vector3(x, 0.5f, y);
    }

     void PerObjectSetup(MapLayerObject layerObject) {
         if (layerObject.name == "spawnpoint") {
             if (layerObject.type == "spawnplayer")
                 _spawnPoint = PlayerPositionFromXY(_map.PixelXToTileX(layerObject.x), _map.PixelYToTileY(layerObject.y));
             if (layerObject.name == "goal") {
             }
         }
         else if (layerObject.name == "trigger") {
             if (layerObject.type == "portal") {
                 Trigger trigger = new Trigger(layerObject.name,
                     layerObject.type,
                     Convert.ToInt32(layerObject.properties.id),
                     _map.PixelXToTileX(layerObject.x),
                     _map.PixelYToTileY(layerObject.y),
                     Convert.ToInt32(layerObject.properties.linkto),
                     layerObject.visible);

                 _triggers.Add(trigger);
             }
         }
    }

    //void PerTileObjectSetup(int xPosition, int yPosition, int tileSetIndex) {
    //    Tile t = _map.TileAtTileSetIndex(tileSetIndex);

    //    if (t.type == "trigger") {
    //        if(t.value == "spawn")
    //            _spawnPoint = PlayerPositionFromXY(xPosition, yPosition);
    //        if (t.value == "goal") {
    //        }
    //    }            
    //}

    void PerTileMapSetup(int xPosition, int yPosition, int tileSetIndex){
        if (tileSetIndex == -1)
            return;

        Tile t = _map.TileAtTileSetIndex(tileSetIndex);

        if (t.type == "wall" && t.value == "block") {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(xPosition, 0.5F, yPosition);
            cube.renderer.material = Resources.Load("CubeWall", typeof(Material)) as Material;
            //cube.transform.localScale = new Vector3(0.9f, 1.0f, 0.9f);

        }
        
        else {
            GameObject quad = NewTileAt(new Vector3(xPosition, 0, yPosition));

            Mesh mesh = quad.GetComponent<MeshFilter>().mesh;
            mesh.uv = _map.UVForTileType(tileSetIndex, mesh);
        }
    }

	void Update () {}

    public void PlayerMoved(Vector3 toPosition) {
        ExecuteNewPosition(toPosition);
    }

    public bool ClampToRectBounds { get; set; }

    public bool RequestPlayerMoveTo(Vector3 position) {
        if (ClampToRectBounds) {
            if (!_map.PositionWithinMapBounds(position))
                return false;
        }

        if (!_crossMyPath) {
            foreach (var quad in playfiedGrid) {
                if (AlmostEqual(quad.Item1, position, 0.25f))
                    return false;
            }
        }

        if (!_crossEmptyTile) {
            if (_map.TileIndexAt(position, MapLayerName.Board) == -1)
                return false;
        }

        if (_map.TileAtMapPosition(position, MapLayerName.Board).type == "wall")
            return false;

        return true;
    }

    public static bool AlmostEqual(Vector3 v1, Vector3 v2, float precision) {
        bool equal = true;

        if (Mathf.Abs(v1.x - v2.x) > precision) equal = false;
        if (Mathf.Abs(v1.y - v2.y) > precision) equal = false;
        if (Mathf.Abs(v1.z - v2.z) > precision) equal = false;

        return equal;
    }

    Mesh CreateQuadMesh() {

        Vector3[] verts = new Vector3[4];
        Vector3[] normals = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] tri = new int[6];

        //MeshFilter mf  = GetComponent(MeshFilter);

        verts[2] = new Vector3(0, 0, 0);
        verts[1] = new Vector3(1, 0, 0);
        verts[0] = new Vector3(0, 0, 1);
        verts[3] = new Vector3(1, 0, 1);

        for (int i = 0; i < normals.Length; i++) {
            normals[i] = Vector3.up;
        }

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(0, 1);
        uv[3] = new Vector2(1, 1);

        tri[0] = 0;
        tri[1] = 2;
        tri[2] = 3;

        tri[3] = 0;
        tri[4] = 3;
        tri[5] = 1;

        Mesh mesh = new Mesh();
        mesh.vertices = verts;
        mesh.triangles = tri;
        mesh.uv = uv;
        mesh.normals = normals;

        Color[] color = new Color[4];
        color[2] = new Color(1, 0, 0);
        color[1] = new Color(0, 1, 0);
        color[0] = new Color(0, 0, 1);
        color[3] = new Color(0, 1, 0);

        mesh.colors = color;

        return mesh;
    }

    GameObject NewTileAt(Vector3 newPlayerPosition) {
        GameObject quad = GameObject.Find("Tile01");

        Vector3 quadPosition = newPlayerPosition;
        quadPosition.y = -0.05f;

        GameObject newQuad = Instantiate(quad, quadPosition, quad.transform.rotation) as GameObject;
        playfiedGrid.Add(Tuple<Vector3, GameObject>.Create(newPlayerPosition, newQuad));

        return newQuad;
    }

    void ExecuteNewPosition(Vector3 newPlayerPosition) {
        if (CreateTrail) {
            GameObject quad = GameObject.Find("Quad2");

            Vector3 quadPosition = newPlayerPosition;
            quadPosition.y = 0;

            GameObject newQuad = Instantiate(quad, quadPosition, quad.transform.rotation) as GameObject;
            playfiedGrid.Add(Tuple<Vector3, GameObject>.Create(newPlayerPosition, newQuad));
        }

        CompositionRoot.Game.ExecutePlayerMove(newPlayerPosition);
    }
}
