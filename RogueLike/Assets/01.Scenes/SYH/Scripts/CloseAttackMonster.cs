using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttackMonster : Monster
{
    private new void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        if (playerDistance < detectDistnce)
        {
            Chasing();
        }
        else
        {
            if (isMove) Moving();
            else Waiting();
        }
    }

    protected override void Attack()
    {
        if (Time.time - lastAttackTime > attackRate)
        {
            rigid.velocity = Vector3.zero;
            isMove = false;
            animator.SetBool("isMove", false);

            lastAttackTime = Time.time;

            animator.SetTrigger("Attack");
            //TODO: 플레이어 대미지 계산
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == gameObject.layer)
        {
            TakeDamage(damage);

            Debug.Log($"체력이 {damage}만큼 달았습니다.");
        }
        
    }
}
