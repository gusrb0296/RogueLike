using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRoom : Room
{
    private int _EnemyCount = 0;

    public override void RoomAction()
    {
        IsVisited = true;
        if (!IsClear)
        {
            CreateEnemy();
        }
        string bgmStr = Random.Range(0, 2) == 0 ? "normalRoomBGM1" : "normalRoomBGM2";
        GameManager.instance.AudioManager.BGM(bgmStr);
    }

    public override void RoomClear()
    {
        base.RoomClear();
    }

    public override void AddTiles()
    {
        base.AddTiles();
    }

    private void CreateEnemy()
    {
        List<Vector2> spawnPositions = new List<Vector2>();
        for (int i = 0; i < tiles.Count; i++)
        {
            foreach(Transform t in tiles[i].transform)
            {
                if (t.gameObject.name == "SpawnPositions")
                {

                    Vector2 pos = new Vector2(t.position.x + transform.position.x, t.position.y + transform.position.y);
                    spawnPositions.Add(pos);
                    _EnemyCount++;
                }
            }
        }
        GameManager.instance.StageManager.GetComponent<SpawnMonsters>().Spawn(spawnPositions, this);
    }

    public void EnemyDie()
    {
        _EnemyCount--;
        if (_EnemyCount == 0)
            RoomClear();
    }
}
