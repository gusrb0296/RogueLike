using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum ERoomType
{
    Battle,
    Reward,
    Boss,
    Clear
}

public class Room : MonoBehaviour
{
    public ERoomType RoomType { get; set; }
    public RoomInfo RoomInfo {  get; set; }
    public MapCreator MapCreator { get; set; }
    public bool IsClear { get; set; }

    public void BreakWall()
    {
        // 오른쪽, 왼쪽, 위, 아래
        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };

        for(int i = 0; i < 4; ++i)
        {
            Vector3Int pos = new Vector3Int(RoomInfo.Position.x + dx[i], RoomInfo.Position.y + dy[i], RoomInfo.Position.z);
            RoomInfo Neighbor = MapCreator.GetRoomInfo(pos);
            if(Neighbor != null)
            {
                if (Neighbor.IsSelected)
                {
                    switch (i)
                    {
                        case 0:
                            RightWallBreak();
                            break;
                        case 1:
                            LeftWallBreak();
                            break;
                        case 2:
                            UpWallBreak();
                            break;
                        case 3:
                            DownWallBreak();
                            break;
                    }
                }
            }
        }
    }

    private void RightWallBreak()
    {
        Transform _wall = transform.Find("Walls");
        Tilemap map = _wall.GetComponent<Tilemap>();
        Vector2 mapSize = MapCreator.MapSize;

        map.SetTile(new Vector3Int((int)mapSize.x / 2 - 1, -1, 0), null);
        map.SetTile(new Vector3Int((int)mapSize.x / 2 - 1, 0, 0), null);
        map.SetTile(new Vector3Int((int)mapSize.x / 2 - 2, -1, 0), null);
        map.SetTile(new Vector3Int((int)mapSize.x / 2 - 2, 0, 0), null);
    }

    private void LeftWallBreak()
    {
        Transform _wall = transform.Find("Walls");
        Tilemap map = _wall.GetComponent<Tilemap>();
        Vector2 mapSize = MapCreator.MapSize;

        map.SetTile(new Vector3Int(-(int)mapSize.x / 2, -1, 0), null);
        map.SetTile(new Vector3Int(-(int)mapSize.x / 2, 0, 0), null);
        map.SetTile(new Vector3Int(-(int)mapSize.x / 2 + 1, -1, 0), null);
        map.SetTile(new Vector3Int(-(int)mapSize.x / 2 + 1, 0, 0), null);
    }

    private void UpWallBreak()
    {
        Transform _ground = transform.Find("Grounds");
        Tilemap map = _ground.GetComponent<Tilemap>();
        Vector2 mapSize = MapCreator.MapSize;

        map.SetTile(new Vector3Int(-1, (int)mapSize.y / 2 - 1, 0), null);
        map.SetTile(new Vector3Int(0, (int)mapSize.y / 2 - 1, 0), null);
        map.SetTile(new Vector3Int(-1, (int)mapSize.y / 2 - 2, 0), null);
        map.SetTile(new Vector3Int(0, (int)mapSize.y / 2 - 2, 0), null);
    }

    private void DownWallBreak()
    {
        Transform _ground = transform.Find("Grounds");
        Tilemap map = _ground.GetComponent<Tilemap>();
        Vector2 mapSize = MapCreator.MapSize;

        map.SetTile(new Vector3Int(-1, -(int)mapSize.y / 2, 0), null);
        map.SetTile(new Vector3Int(0, -(int)mapSize.y / 2, 0), null);
        map.SetTile(new Vector3Int(-1, -(int)mapSize.y / 2 + 1, 0), null);
        map.SetTile(new Vector3Int(0, -(int)mapSize.y / 2 + 1, 0), null);
    }
}
