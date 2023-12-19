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
        GameManager.instance.StageManager.OnStageScene += Init;
        GameManager.instance.StageManager.OnStageScene += Spawn;
    }

    private void Init()
    {
        spawnPositionObjects = GameObject.FindGameObjectsWithTag("Spawn").ToList();
    }

    private void Spawn()
    {
        if(spawnPositionObjects!= null)
        {
            foreach (GameObject pos in spawnPositionObjects)
            {
                int idx = Random.Range(0, monsters.Count);
                GameObject monster = Instantiate(monsters[idx]);
                monster.transform.position = pos.transform.position;
            }
        }
        
    }
}
