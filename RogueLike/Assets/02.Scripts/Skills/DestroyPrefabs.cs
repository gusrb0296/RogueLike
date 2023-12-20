using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPrefabs : MonoBehaviour
{
    [SerializeField] private float _destroyTime;
    void Start()
    {
        Destroy(gameObject, _destroyTime);
    }

}
