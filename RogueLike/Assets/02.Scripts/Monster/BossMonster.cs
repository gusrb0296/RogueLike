using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour, IDamagable
{
    #region Factors
    [Header("Stats")]
    public float health;
    public float speed;

    [Header("Reward")]
    public int gold;
    public int exp;

    [Header("Combat")]
    public float damage;
    public float attackDistance;
    public float rangedAttackDistance;
    public float attackRate;
    private float lastAttackTime;
    private bool isAttacking = false;

    [Header("Projectile")]
    public GameObject projectile;
    public float projectileSpeed;
    public Transform muzzle;

    private float playerDistance;
    private Vector2 playerDirection;

    private Animator animator;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    private GameObject player;
    private bool canReceiveInput = true;
    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        playerDirection = (player.transform.position - transform.position).normalized;
        spriteRenderer.flipX = playerDirection.x < 0;
        if (canReceiveInput)
        {
            if (isAttacking)
            {
                rigid.velocity = Vector2.zero;
            }
            else
            {
                Chase();
            }
        }
    }

    private void Chase()
    {
        rigid.velocity = playerDirection.normalized * speed;
        
        if(playerDistance < rangedAttackDistance)
        {
            if (Time.time - lastAttackTime > attackRate)
            {
                lastAttackTime = Time.time;
                int attackIdx = Random.Range(0, 3);
                switch (attackIdx)
                {
                    case 0:
                        Attack();
                        break;

                    case 1:
                        Attack2();
                        break;

                    case 2:
                        RangedAttack();
                        break;
                }
            }
        }
    }

    private void Attack()
    {
        if (playerDistance < attackDistance)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }

    private void Attack2()
    {
        if (playerDistance < attackDistance)
        {
            isAttacking = true;
            animator.SetTrigger("Attack2");
        }
    }

    private void RangedAttack()
    {
        isAttacking = true;
        animator.SetTrigger("RangedAttack");
        StartCoroutine("Fire");
    }

    private IEnumerator Fire()
    {
        GameObject tile = Instantiate(projectile);
        tile.transform.position = muzzle.transform.position;
       
        yield return new WaitForSeconds(0.5f);

        Vector2 direction = (player.transform.position - muzzle.transform.position).normalized;
        Vector2 fireDirection = direction * projectileSpeed;
        if (tile != null)
        {
            tile.GetComponent<Rigidbody2D>().velocity = fireDirection;
        }

        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("Hit");

        health = health - damage > 0 ? health - damage : 0;
        Debug.Log($"체력이 {damage}만큼 달았습니다.");
        if (health == 0) StartCoroutine("Die");

        isAttacking = false;
    }

    public void OnDamage()
    {
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

    IEnumerator Die()
    {
        animator.SetTrigger("Die");
        yield return new WaitForSecondsRealtime(1f);
        Destroy(gameObject);
    }
}
