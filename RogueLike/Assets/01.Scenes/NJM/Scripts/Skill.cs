using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public SkillItemData SkillData;

    private void Start()
    {
        // 플레이어 스테미너 감소
        Debug.Log("스테미나 " + SkillData.Stamina + " 감소");
    }

    private void Update()
    {
        transform.position += new Vector3(1, 0, 0) * SkillData.SkillSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적에 맞으면 데미지
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
            Debug.Log("적 Hp " + SkillData.Atk + " 감소");
            Instantiate(SkillData.SkillEffect, gameObject.transform.position + SkillData.SkillEffectPosition, Quaternion.identity);
        }
        else if(collision.tag == "Obstacle")
        {
            Destroy(gameObject);
            Instantiate(SkillData.SkillMissEffect, gameObject.transform.position + SkillData.SkillEffectPosition, Quaternion.identity);
        }

    }

}
