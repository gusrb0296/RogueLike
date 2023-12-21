using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : Room
{
    public override void RoomAction()
    {
        IsVisited = true;
        GameManager.instance.AudioManager.BGM("bossRoomBGM");
        if (!IsClear)
        {
            SpawnBoss();
        }
    }

    public override void RoomClear()
    {
        base.RoomClear();
        GameObject go = Resources.Load<GameObject>("Prefabs/Map/EndPortalObj");
        GameObject endPortal = Instantiate(go, transform);
        endPortal.transform.position = new Vector2(endPortal.transform.position.x, endPortal.transform.position.y + 0.6f);
    }

    public override void AddTiles()
    {

    }

    public void SpawnBoss()
    {
        GameObject boss = GameManager.instance.StageManager.GetComponent<SpawnMonsters>().
            SpawnBoss(new Vector2(transform.position.x, transform.position.y + 1), this);

        GameObject bossProgressBar = Resources.Load<GameObject>("Prefabs/Map/UI/BossProgressBar");
        GameObject go = Instantiate(bossProgressBar);
        go.GetComponent<BossHPUI>().SetTarget(boss);
    }
}
