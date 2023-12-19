using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public SkillItemData SkillData;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // 플레이어 스테미너 감소
        Debug.Log("스테미나 " + SkillData.Stamina + " 감소");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            int dir = (_rigidbody.velocity.x > 0) ? 1 : -1;
            Destroy(gameObject);          

            Instantiate(SkillData.SkillEffect, gameObject.transform.position + new Vector3(SkillData.SkillPosition.x * dir, SkillData.SkillPosition.y, SkillData.SkillPosition.z), Quaternion.identity);

            // TODO
            Debug.Log("적 Hp " + SkillData.Atk + " 감소");
        }
        else if(collision.tag == "Obstacle")
        {
            int dir = (_rigidbody.velocity.x > 0) ? 1 : -1;
            Destroy(gameObject);
            
            Instantiate(SkillData.SkillMissEffect, gameObject.transform.position + new Vector3(SkillData.SkillPosition.x * dir, SkillData.SkillPosition.y, SkillData.SkillPosition.z), Quaternion.identity);
        }

    }

    public void SkillShoot(Vector2 direction)
    {
        if (direction.x < 0) _spriteRenderer.flipX = _spriteRenderer.flipX ? false : true;
        _rigidbody.velocity = direction * SkillData.SkillSpeed;
    }
}
