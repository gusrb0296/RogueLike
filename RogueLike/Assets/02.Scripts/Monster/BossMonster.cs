using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour, IDamagable
{
    #region Factors
    public BattleRoom room;

    [Header("Stats")]
    public float health;
    private float currentHealth;
    public float speed;

    [Header("Reward")]
    public int gold;

    [Header("Combat")]
    public float damage;
    public float attackDistance;
    public float rangedAttackDistance;
    public float attackRate;
    private float lastAttackTime;
    private bool isAttacking = false;
    private bool isDie = false;

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
        currentHealth = health;
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
                switch (Random.Range(0, 3))
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
        GameManager.instance.AudioManager.SFX("bossShortATK");
        if (playerDistance < attackDistance)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }

    private void Attack2()
    {
        GameManager.instance.AudioManager.SFX("bossShortATK");
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
        tile.GetComponent<Projectile>().damage = this.damage;
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.AudioManager.SFX("bossLongATK");
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
        if(!isDie)
        {
            animator.SetTrigger("Hit");
            GameManager.instance.AudioManager.SFX("monsterHit");

            //����ȭ
            if (currentHealth >= health / 2 && currentHealth - damage <= health / 2)
            {
                spriteRenderer.color = new Color(1f, .5f, .5f);
                this.damage += 5;
                attackRate -= 0.25f;
                speed += 2;
            }

            currentHealth = currentHealth - damage > 0 ? currentHealth - damage : 0;
            Debug.Log($"ü���� {damage}��ŭ �޾ҽ��ϴ�.");
            if (currentHealth == 0) StartCoroutine(nameof(Die));

            isAttacking = false;
        }
    }

    public void OnDamage()
    {
        GameManager.instance.AudioManager.SFX("bossShortATK");
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

    public float GetHpRate()
    {
        return currentHealth / health;
    }

    IEnumerator Die()
    {
        animator.SetTrigger("Die");
        isDie = true;
        yield return new WaitForSecondsRealtime(.7f);
        GameManager.instance.UiManager.GameClearAnim();
        Destroy(gameObject);
    }
}
