using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRoom : Room
{
    private int _EnemyCount = 0;

    public override void RoomAction()
    {
        if (!IsClear)
        {
            CreateEnemy();
        }
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
        List<Transform> spawnPositions = new List<Transform>();
        for (int i = 0; i < tiles.Count; i++)
        {
            foreach(Transform t in tiles[i].transform)
            {
                if (t.gameObject.name == "SpawnPositions")
                {
                    spawnPositions.Add(t);
                    _EnemyCount++;
                }
            }
        }

        //TODO 몬스터 생성
    }

    public void EnemyDie()
    {
        _EnemyCount--;
        if (_EnemyCount == 0)
            RoomClear();
    }
}
