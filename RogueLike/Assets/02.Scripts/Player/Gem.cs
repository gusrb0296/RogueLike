using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public float DestroyTime;
    public int Gold;
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
            GameManager.instance.DataManager.UpdatePlayerCurrentGold(Gold);
            Debug.Log("°ñµå È¹µæ! +"+Gold);
            Destroy(gameObject);
        }
    }

}
