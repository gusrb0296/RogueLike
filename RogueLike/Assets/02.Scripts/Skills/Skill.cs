using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public SkillItemData SkillData;
    [SerializeField] private DamageText _damageText;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameManager.instance.AudioManager.SFX(SkillData.SkillSound);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            GameManager.instance.AudioManager.SFX(SkillData.SkillDamagedSound);

            int dir = (_rigidbody.velocity.x > 0) ? 1 : -1;
            Instantiate(SkillData.SkillEffect, gameObject.transform.position + new Vector3(SkillData.SkillPosition.x * dir, SkillData.SkillPosition.y, SkillData.SkillPosition.z), Quaternion.identity);
            _damageText.CreateSkillText(SkillData, gameObject.transform, _rigidbody.velocity.x);

            collision.gameObject.GetComponent<IDamagable>().TakeDamage(SkillData.Atk);

            Destroy(gameObject);
        }
        else if(collision.tag == "Obstacle")
        {
            int dir = (_rigidbody.velocity.x > 0) ? 1 : -1;
            
            Instantiate(SkillData.SkillMissEffect, gameObject.transform.position + new Vector3(SkillData.SkillPosition.x * dir, SkillData.SkillPosition.y, SkillData.SkillPosition.z), Quaternion.identity);
            
            Destroy(gameObject);
        }

    }

    public void SkillShoot(Vector2 direction)
    {
        if (direction.x < 0) _spriteRenderer.flipX = _spriteRenderer.flipX ? false : true;
        _rigidbody.velocity = direction * SkillData.SkillSpeed;
    }
}
