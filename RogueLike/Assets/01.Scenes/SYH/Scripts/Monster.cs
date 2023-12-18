using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamagable
{
    //Stat SO 만들어지면 대체하려고 함.
    #region Factors
    [Header("Stats")]
    public float health;
    public float walkspeed;
    public float runSpeed;

    [Header("Reward")]
    public int gold;
    public int exp;

    [Header("Combat")]
    public float damage;
    public float attackRate;
    protected float lastAttackTime;
    public float detectDistnce;
    public float attackDistance;
    protected bool isAttacking = false;

    [Header("Wandering")]
    public float wanderWaitTime;
    public float wanderMaxTime;
    protected float wanderingTime = 0;
    protected float waitingTime = 0;
    protected bool isLeft, isMove;

    protected float playerDistance;

    protected Animator animator;
    protected Rigidbody2D rigid;
    protected SpriteRenderer spriteRenderer;

    //임시로 사용할 예정
    protected GameObject player;
    #endregion

    #region LifeCycles
    protected void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    #endregion

    #region MonsterStates
    protected void Chasing()
    {
        if (playerDistance < attackDistance)
        {
            Attack();
        }
        else
        {
            Run();
        }

    }

    protected virtual void Attack() { }

    protected void Run()
    {
        Vector2 playerDirection = player.transform.position - transform.position;
        isMove = true;
        animator.SetBool("isMove", true);

        if (playerDirection.x < 0)   //왼쪽에 있음
        {
            spriteRenderer.flipX = true;
            rigid.velocity = Vector2.left * runSpeed;


        }
        else //오른쪽
        {
            spriteRenderer.flipX = false;
            rigid.velocity = Vector2.right * runSpeed;
        }
    }

    protected void Waiting()
    {
        if (waitingTime > wanderWaitTime)
        {
            isMove = true;
            animator.SetBool("isMove", true);
            waitingTime = 0;
        }
        else
        {
            waitingTime += Time.deltaTime;
        }
    }

    protected void Moving()
    {
        if (isLeft)
        {
            if (wanderingTime > wanderMaxTime)
            {
                wanderingTime = 0;
                isLeft = false;
                spriteRenderer.flipX = false;

                isMove = false;
                animator.SetBool("isMove", false);
                rigid.velocity = Vector2.zero;
            }
            else
            {
                wanderingTime += Time.deltaTime;
                rigid.velocity = Vector2.left * walkspeed;
            }

        }
        else
        {
            if (wanderingTime > wanderMaxTime)
            {
                wanderingTime = 0;
                isLeft = true;
                spriteRenderer.flipX = true;

                isMove = false;
                animator.SetBool("isMove", false);
                rigid.velocity = Vector2.zero;
            }
            else
            {
                wanderingTime += Time.deltaTime;
                rigid.velocity = Vector2.right * walkspeed;
            }
        }

    }

    public void TakeDamage(float damage)
    {
        //넉백효과
        health = health - damage > 0 ? health - damage : 0;
        Debug.Log($"체력이 {damage}만큼 달았습니다.");
        if (health == 0) StartCoroutine("Die");
    }

    IEnumerator Die()
    {
        animator.SetTrigger("Die");
        yield return new WaitForSecondsRealtime(1f);
        Destroy(gameObject);
    }
    #endregion
}
