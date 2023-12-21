using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureRoom : Room
{
    public override void RoomAction()
    {
        base.RoomAction();
        GameManager.instance.AudioManager.BGM("shopRoomBGM");
    }

    public override void RoomClear()
    {
        base.RoomClear();
    }

    public override void AddTiles()
    {
        base.AddTiles();
    }
}
