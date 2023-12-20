using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttackMonster : Monster
{
    
    private new void Awake()
    {
        base.Awake();
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Attack()
    {
        if (Time.time - lastAttackTime > attackRate)
        {
            rigid.velocity = Vector2.zero;
            isMove = false;
            animator.SetBool("isMove", false);

            lastAttackTime = Time.time;

            isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }

    public void OnDamage()
    {
        if(gameObject.name.Contains("Minotaur"))
        {
            GameManager.instance.AudioManager.SFX("monsterAxeATK");
        }
        else if (gameObject.name.Contains("Slayer"))
        {
            GameManager.instance.AudioManager.SFX("monsterUpperATK");
        }
        else if (gameObject.name.Contains("Skeleton"))
        {
            GameManager.instance.AudioManager.SFX("monsterSwordATK");
        }
        
        if (playerDistance < attackDistance)
        {
            GameManager.instance.DataManager.Player.GetComponent<PlayerCollision>().TakeDamage(damage);
            Debug.Log($"hit {damage} to player");
        }
    }

    public void AttackEnd()
    {
        isAttacking = false;
    }

    
}
