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

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(AutoDestroy());
    }

    public void Shoot(Vector2 direction)
    {
        if(direction.x < 0) _spriteRenderer.flipX = true;
        _rigidbody.velocity = direction * speed;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
    
    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(3f);
        DestroyBullet();
    }
}
