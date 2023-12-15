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

            animator.SetTrigger("RangeAttack");
            StartCoroutine("Fire");
        }
    }

    private IEnumerator Fire()
    {
        GameObject tile = Instantiate(projectile);
        tile.transform.position = muzzle.position;
        Vector2 direction = player.transform.position - muzzle.transform.position;
        Vector2 fireDirection = direction * projectileSpeed;
        yield return new WaitForSecondsRealtime(1.5f);
        tile.GetComponent<Rigidbody2D>().velocity = fireDirection;
    }

}