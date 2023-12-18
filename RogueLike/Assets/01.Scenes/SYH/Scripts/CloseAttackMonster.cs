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
        if (isAttacking)
        {
            rigid.velocity = Vector2.zero;
        }
        else if (playerDistance < detectDistnce)
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

            isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }

    public void onDamage()
    {
        if (playerDistance < attackDistance)
        {
            //TODO: 플레이어 대미지 계산
            Debug.Log($"hit {damage} to player");
        }
    }

    public void AttackEnd()
    {
        isAttacking = false;
    }
}
