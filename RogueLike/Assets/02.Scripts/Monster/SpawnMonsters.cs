using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnMonsters : MonoBehaviour
{
    private List<GameObject> monsters = new();
    private List<GameObject> spawnPositionObjects = new();

    private void Awake()
    {
        monsters = Resources.LoadAll<GameObject>("Prefabs\\Monsters").ToList();
    }

    private void Start()
    {
        
    }

    private void Init()
    {
        spawnPositionObjects = GameObject.FindGameObjectsWithTag("Spawn").ToList();
    }

    public void Spawn(GameObject[] spawnpositions)
    {
        if(spawnpositions != null)
        {
            foreach (GameObject pos in spawnpositions)
            {
                int idx = Random.Range(0, monsters.Count);
                GameObject monster = Instantiate(monsters[idx]);
                monster.transform.position = pos.transform.position;
            }
        }
        
    }
}
