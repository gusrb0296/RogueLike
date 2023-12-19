using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
    }

    public void Shoot(Vector2 direction, float range)
    {
        if(direction.x < 0) _spriteRenderer.flipX = true;
        _rigidbody.velocity = direction * speed;
        StartCoroutine(RangeDestroy(range / speed));
    }

    IEnumerator RangeDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
    
    IEnumerator AutoDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        DestroyBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _rigidbody.velocity = Vector2.zero;
            _animator.SetTrigger("Hit");
            AutoDestroy(0.1f);
        }
    }
}
