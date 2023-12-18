using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask target;
    public LayerMask map;
    private Animator animator;
    private float Lifetime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Lifetime = Time.time;
    }

    private void Update()
    {
        if (Time.time - Lifetime > 3f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (target.value == (target.value | (1 << collision.gameObject.layer)))
        {
            //TODO: ³ªÁß¿¡ Player ÄÄÆ÷³ÍÆ®·Î ¹Ù²ãÁà¾ß ÇÔ
            //collision.gameObject.GetComponent<CloseAttackMonster>().TakeDamage(10);
            animator.SetTrigger("Destroy");
        }

        if (map.value == (map.value | (1 << collision.gameObject.layer)))
        {
            animator.SetTrigger("Destroy");
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
