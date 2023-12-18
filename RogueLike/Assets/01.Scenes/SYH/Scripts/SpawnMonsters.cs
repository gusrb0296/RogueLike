using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonsters : MonoBehaviour
{
    public List<GameObject> monsters = new List<GameObject>();
    public Transform spawnPositionRoot;
    private List<Transform> spawnPositions = new List<Transform>();

    public GameObject player;

    private void Awake()
    {
        for(int i = 0; i<spawnPositionRoot.childCount; i++)
        {
            spawnPositions.Add(spawnPositionRoot.GetChild(i));
        }
    }

    private void Start()
    {
        GameObject p = Instantiate(player);
        foreach (Transform pos in spawnPositions)
        {
            int idx = Random.Range(0, monsters.Count);
            GameObject monster = Instantiate(monsters[idx]);
            monster.transform.position = pos.position;
        }

    }
}
