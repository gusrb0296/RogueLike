using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    private Vector2 _movementInput;

    [Header("Animation")]
    private Animator _animator;

    [Header("Jump")]
    public float jumpForce;
    public LayerMask groundLayerMask;
    private Vector2 boxCastSize = new Vector2(0.4f, 0.05f);
    private float boxCastMaxDistance = 0.7f;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        Move();

        if (_rigidbody.velocity.y < 0)
        {
            _animator.SetBool("Jump", false);
            _animator.SetBool("Fall", true);
            if (OnGround())
            {
                _animator.SetBool("Fall", false);
            }
        }
    }

    private void Move()
    {
        Vector2 dir = _movementInput;
        dir *= speed;
        dir.y = _rigidbody.velocity.y;

        if (dir.x > 0) _spriteRenderer.flipX = false;
        else if (dir.x < 0) _spriteRenderer.flipX = true;

        _rigidbody.velocity = dir;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _movementInput = context.ReadValue<Vector2>();
            _animator.SetBool("IsMove", true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _movementInput = Vector2.zero;
            _animator.SetBool("IsMove", false);
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && OnGround())
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _animator.SetTrigger("Jump");
        }
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _animator.SetTrigger("Shoot");
        }
    }

    private bool OnGround()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, boxCastSize, 0f, Vector2.down, boxCastMaxDistance, groundLayerMask);
        return (raycastHit.collider != null);
    }
}
