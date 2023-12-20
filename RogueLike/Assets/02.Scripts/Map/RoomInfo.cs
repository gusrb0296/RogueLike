using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.ParticleSystem;

public class RoomInfo
{
    public Room Owner { get; set; }
    public Vector3Int Position { get; private set; }
    public bool IsSelected { get; set; }
    public ERoomType RoomType { get; set; }

    public RoomInfo(Vector3Int position)
    {
        Position = position;
    }
}
