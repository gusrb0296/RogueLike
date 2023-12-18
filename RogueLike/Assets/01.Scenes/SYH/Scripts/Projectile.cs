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
    private float damage = 10;

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

            //TODO 플레이어 대미지 입히기
            Debug.Log($"hit {damage} to player");
            animator.SetTrigger("Destroy");
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

        if (map.value == (map.value | (1 << collision.gameObject.layer)))
        {
            animator.SetTrigger("Destroy");
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
