using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public float DestroyTime;

    private void Start()
    {
        StartCoroutine(AutoDestroy(DestroyTime));
    }

    IEnumerator AutoDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 보유 골드 증가
            Destroy(gameObject);
        }
    }

}
