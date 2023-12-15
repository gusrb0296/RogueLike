using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectItem : MonoBehaviour
{
    public EffectItemData EffectData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            UseItem();
        }
    }

    public void UseItem()
    {
        // 플레이어 체력, 스테미너 증가 코드
        Debug.Log("플레이어 Hp " + EffectData.Hp + " 회복");
        Debug.Log("플레이어 Stamina " + EffectData.Stamina + " 회복");

        Destroy(gameObject);
    }
}
