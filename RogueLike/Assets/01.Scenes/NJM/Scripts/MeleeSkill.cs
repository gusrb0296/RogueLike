using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkill : MonoBehaviour
{
    public SkillItemData SkillData;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    //[SerializeField] private PlayerController _playerController;

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
            //Instantiate(SkillData.SkillEffect, gameObject.transform.position + new Vector3(SkillData.SkillPosition.x , SkillData.SkillPosition.y, SkillData.SkillPosition.z), Quaternion.identity);

            // TODO
            Debug.Log("적 Hp " + SkillData.Atk + " 감소");
        }
    }

    public void SkillShoot(Vector2 direction)
    {
        if (direction.x < 0)
        {
            if (_spriteRenderer.flipX == true) _spriteRenderer.flipX = false;
            else _spriteRenderer.flipX = true;
        }
    }
}
