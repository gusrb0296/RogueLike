using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnMonsters : MonoBehaviour
{
    private List<GameObject> monsters = new();
    private GameObject boss; 

    private void Awake()
    {
        monsters = Resources.LoadAll<GameObject>("Prefabs\\Monsters").ToList();
        boss = Resources.Load<GameObject>("Prefabs\\Boss\\Flying Eye");
    }

    public void Spawn(List<Vector2> spawnpositions, BattleRoom room)
    {
        if (spawnpositions != null)
        {
            foreach (Vector2 pos in spawnpositions)
            {
                int idx = Random.Range(0, monsters.Count);
                GameObject monster = Instantiate(monsters[idx]);
                monster.transform.position = pos;
                monster.GetComponent<Monster>().room = room;
            }
        }
    }
    public void SpawnBoss(Vector2 spawnpositions, Room room)
    {
        if (spawnpositions != null)
        {
            GameObject monster = Instantiate(boss);
            monster.transform.position = spawnpositions;
            monster.GetComponent<BossMonster>().room = room;
        }
    }
}
