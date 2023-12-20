using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : Room
{
    public override void RoomAction()
    {
        IsVisited = true;
        GameManager.instance.AudioManager.BGM("bossRoomBGM");
    }

    public override void RoomClear()
    {

    }

    public override void AddTiles()
    {

    }
}
