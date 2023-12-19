using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monster : MonoBehaviour, IDamagable
{
    public BattleRoom room;
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
    public LayerMask groundLayer;
    protected float wanderingTime = 0;
    protected float waitingTime = 0;
    protected bool isMove;
    Vector2 randomDirection;

    protected float playerDistance;
    protected Vector2 playerDirection;
    protected Vector2 moveDirection;

    protected Animator animator;
    protected Rigidbody2D rigid;
    protected SpriteRenderer spriteRenderer;
    protected bool canReceiveInput = true;
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

    protected void FixedUpdate()
    {
        playerDirection = player.transform.position - transform.position;
        moveDirection = playerDirection.x > 0 ? Vector2.right : Vector2.left;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(10);
        }
        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        spriteRenderer.flipX = playerDirection.x <= 0;
        if (canReceiveInput)
        {
            if (isAttacking)
            {
                rigid.velocity = Vector2.zero;
            }
            else
            {
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
        }
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
        isMove = true;
        animator.SetBool("isMove", true);
        rigid.velocity = moveDirection * runSpeed;
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
        if (wanderingTime > wanderMaxTime)
        {
            wanderingTime = 0;
            int randomDir = Random.Range(0, 2);
            randomDirection = randomDir == 0 ? Vector2.right : Vector2.left;
            isMove = false;
            animator.SetBool("isMove", false);
            rigid.velocity = Vector2.zero;
        }
        else
        {
            wanderingTime += Time.deltaTime;
            Vector2 startPos = (Vector2)transform.position + Vector2.up * 2;
            Vector2 rayDirection = new Vector2(randomDirection.x, -Mathf.Abs(randomDirection.x) * 3);
            RaycastHit2D hit = Physics2D.Raycast(startPos, rayDirection, 5, groundLayer);
            Debug.DrawRay(startPos, hit.point, Color.yellow);
            if(hit.collider != null)
            {
                rigid.velocity = randomDirection * walkspeed;
            }
        }

    }

    public void TakeDamage(float damage)
    {
        Vector2 knockback = -moveDirection * 10 + Vector2.up * 10;
        rigid.AddForce(knockback, ForceMode2D.Impulse);
        StartCoroutine(BlockInputForTime(.5f));
        animator.SetTrigger("Hit");

        health = health - damage > 0 ? health - damage : 0;
        Debug.Log($"체력이 {damage}만큼 달았습니다.");
        if (health == 0) StartCoroutine("Die");

        isAttacking = false;
    }

    IEnumerator BlockInputForTime(float blockTime)
    {
        canReceiveInput = false;

        yield return new WaitForSeconds(blockTime);

        canReceiveInput = true;
    }

    IEnumerator Die()
    {
        animator.SetTrigger("Die");
        canReceiveInput = false;
        yield return new WaitForSecondsRealtime(1f);
        room.EnemyDie();
        if (Random.Range(0, 3) == 0)
        {
            GameObject gem = Resources.Load<GameObject>("Prefabs\\Items\\Gem");
            gem.GetComponent<Gem>().Gold = Random.Range(10, 20);
            Instantiate(gem).transform.position = transform.position;

        }
        Destroy(gameObject);
    }
    #endregion
}
