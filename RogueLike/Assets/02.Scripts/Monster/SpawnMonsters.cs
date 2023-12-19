using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnMonsters : MonoBehaviour
{
    public List<GameObject> monsters = new();
    private List<GameObject> spawnPositionObjects = new();

    private void Awake()
    {
        //monsters = Resources.LoadAll<GameObject>("Prefabs\\Monsters").ToList();
    }

    private void Start()
    {
        
    }

    private void Init()
    {
        spawnPositionObjects = GameObject.FindGameObjectsWithTag("Spawn").ToList();
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
