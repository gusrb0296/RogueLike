using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnMonsters : MonoBehaviour
{
    public List<GameObject> monsters = new();
    private int monsterCount = 4;
    private void Awake()
    {
        //for(int i = 0; i < monsterCount; i++)
        //{
        //    monsters.Add(Resources.Load<GameObject>("Prefabs\\Monsters\\Slayer"));
        //}
        monsters = Resources.LoadAll<GameObject>("Prefabs\\Monsters").ToList();
    }

    private void Start()
    {
        
    }

    public void Spawn(List<Vector2> spawnpositions, BattleRoom room)
    {
        if(spawnpositions != null)
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
}
