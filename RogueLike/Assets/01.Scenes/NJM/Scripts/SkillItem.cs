using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour
{
    public SkillItemData SkillData;
    public PlayerNJM Player;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UseItem();
        }
    }

    void UseItem()
    {
        Player.IsSkill = true;
        Player.SkillData = SkillData;
        Destroy(gameObject);
    }

}
