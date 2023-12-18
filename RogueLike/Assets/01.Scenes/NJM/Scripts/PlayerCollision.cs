using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public SkillItemData SkillData;

    private void Awake()
    {
        SkillData = null;
    }

    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // SkillItem ÀÏ °æ¿ì
        if(collision.gameObject.GetComponent<SkillItem>() && collision.gameObject.GetComponent<SkillItem>().SkillData.Type == ItemType.Skiil)
        {
            SkillData = collision.gameObject.GetComponent<SkillItem>().SkillData;
        }

    }
}
