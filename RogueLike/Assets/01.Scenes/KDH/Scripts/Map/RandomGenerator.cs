using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum EMapDir
{
    Right = 1 << 0,
    Left = 1 << 1,
    Up = 1 << 2,
    Dwon = 1 << 3
}

public class RandomGenerator : MonoBehaviour
{
    [SerializeField] private int mapCountX;
    [SerializeField] private int mapCountY;

    [SerializeField] private int mapSizeX;
    [SerializeField] private int mapSizeY;

    [SerializeField] private GameObject testGrid;
    [SerializeField] private GameObject testTileMap;

    private List<MapInfo> mapInfos = new List<MapInfo>();
    private List<GameObject> tileMaps = new List<GameObject>();

    public TileBase tileBase;
    public int moveType;

    private void Start()
    {
        Vector3 mapPos = Vector3.zero;
        for(int y = 0; y < mapCountY; y++)
        {
            for(int x = 0; x < mapCountX; x++)
            {
                GameObject map = Instantiate(testTileMap, testGrid.gameObject.transform);
                mapPos.Set(mapSizeX * x, mapSizeY * y, 0f);
                tileMaps.Add(map);
                map.transform.localPosition = mapPos;
            }
        }
        moveType = 0b1111;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            for (int i = 0; i < tileMaps.Count; ++i)
            {
                Tilemap map = tileMaps[i].GetComponent<Tilemap>();

                int n = (int)EMapDir.Right & moveType;
                if ((int)EMapDir.Right == n)
                    RightWallBreak(map);

                n = (int)EMapDir.Left & moveType;
                if ((int)EMapDir.Left == n)
                    LeftWallBreak(map);

                n = (int)EMapDir.Up & moveType;
                if ((int)EMapDir.Up == n)
                    UpWallBreak(map);

                n = (int)EMapDir.Dwon & moveType;
                if ((int)EMapDir.Dwon == n)
                    DownWallBreak(map);
            }
        }
    }

    private void RightWallBreak(Tilemap map)
    {
        map.SetTile(new Vector3Int(mapSizeX / 2 - 1, -1, 0), null);
        map.SetTile(new Vector3Int(mapSizeX / 2 - 1, 0, 0), null);
        map.SetTile(new Vector3Int(mapSizeX / 2 - 2, -1, 0), null);
        map.SetTile(new Vector3Int(mapSizeX / 2 - 2, 0, 0), null);
    }

    private void LeftWallBreak(Tilemap map)
    {
        map.SetTile(new Vector3Int(-mapSizeX / 2, -1, 0), null);
        map.SetTile(new Vector3Int(-mapSizeX / 2, 0, 0), null);
        map.SetTile(new Vector3Int(-mapSizeX / 2 + 1, -1, 0), null);
        map.SetTile(new Vector3Int(-mapSizeX / 2 + 1, 0, 0), null);
    }

    private void UpWallBreak(Tilemap map)
    {
        map.SetTile(new Vector3Int(-1, mapSizeY / 2 - 1, 0), null);
        map.SetTile(new Vector3Int(0, mapSizeY / 2 - 1, 0), null);
        map.SetTile(new Vector3Int(-1, mapSizeY / 2 - 2, 0), null);
        map.SetTile(new Vector3Int(0, mapSizeY / 2 - 2, 0), null);
    }

    private void DownWallBreak(Tilemap map)
    {
        map.SetTile(new Vector3Int(-1, -mapSizeY / 2, 0), null);
        map.SetTile(new Vector3Int(0, -mapSizeY / 2, 0), null);
        map.SetTile(new Vector3Int(-1, -mapSizeY / 2 + 1, 0), null);
        map.SetTile(new Vector3Int(0, -mapSizeY / 2 + 1, 0), null);
    }
}
