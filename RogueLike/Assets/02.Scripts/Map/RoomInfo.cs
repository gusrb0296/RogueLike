using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.ParticleSystem;

public class RoomInfo
{
    public Vector3Int Position { get; set; }
    public bool IsSelected { get; set; }

    public RoomInfo(Vector3Int position)
    {
        Position = position;
    }
}
