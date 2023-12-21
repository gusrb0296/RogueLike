using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour
{
    public SkillItemData SkillData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UseItem();
        }
    }

    void UseItem()
    {
        GameManager.instance.UiManager.SkillIcon.sprite = SkillData.SkillIcon;
        GameManager.instance.AudioManager.SFX(SkillData.ItemSound);

        Destroy(gameObject);
    }

}
