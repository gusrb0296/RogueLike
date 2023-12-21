using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackMonster : Monster
{
    [Header("Projectile")]
    public GameObject projectile;
    public float projectileSpeed;
    public Transform muzzle;

    private new void Awake()
    {
        base.Awake();
    }

    private void FixedUpdate()
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

            animator.SetTrigger("RangeAttack");
            StartCoroutine(nameof(Fire));
            animator.SetBool("isMove", false);
            isAttacking = true;
        }
    }

    private IEnumerator Fire()
    {
        GameObject tile = Instantiate(projectile);
        tile.transform.position = muzzle.position;
        Vector2 direction = player.transform.position - muzzle.transform.position;
        Vector2 fireDirection = direction * projectileSpeed;


        yield return new WaitForSecondsRealtime(1f);
        GameManager.instance.AudioManager.SFX("monsterFireATK");

        GameManager.instance.AudioManager.SFX("monsterFireATK");
        if (tile != null)
        {
            tile.GetComponent<Rigidbody2D>().velocity = fireDirection;
        }
        isAttacking = false;
    }

}
