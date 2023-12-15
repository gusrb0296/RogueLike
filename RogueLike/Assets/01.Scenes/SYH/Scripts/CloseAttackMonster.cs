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

        }
    }
}
