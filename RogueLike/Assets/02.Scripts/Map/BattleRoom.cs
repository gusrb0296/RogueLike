using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRoom : Room
{
    List<Vector2> spawnPositions = new List<Vector2>();
    List<Vector2> spawnItemPositions = new List<Vector2>();
    private int _EnemyCount = 0;

    private void Start()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            foreach (Transform t in tiles[i].transform)
            {
                Vector2 pos = new Vector2(t.position.x + transform.position.x, t.position.y + transform.position.y);
                if (t.gameObject.name == "SpawnPositions")
                {
                    spawnPositions.Add(pos);
                    _EnemyCount++;
                }
                else if(t.gameObject.name == "ItemSpawnPos")
                {
                    spawnItemPositions.Add(pos);
                }
            }
        }
    }

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
        CreateSkillItem();
    }

    public override void AddTiles()
    {
        base.AddTiles();
    }

    public void EnemyDie()
    {
        _EnemyCount--;
        if (_EnemyCount == 0)
            RoomClear();
    }

    private void CreateEnemy()
    {
        GameManager.instance.StageManager.GetComponent<SpawnMonsters>().Spawn(spawnPositions, this);
    }

    private void CreateSkillItem()
    {
        List<GameObject> skillItemPrefabs = GameManager.instance.DataManager.skillItemPrefabs;

        int skillItemPrefabsIdx = Random.Range(0, skillItemPrefabs.Count);
        int spawnItemPositionsIdx = Random.Range(0, spawnItemPositions.Count);

        Instantiate(skillItemPrefabs[skillItemPrefabsIdx], spawnItemPositions[spawnItemPositionsIdx],
            Quaternion.identity, transform);
    }
}
