using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class PlayerController : MonoBehaviour
{
    #region Field
    [Header("Movement")]
    //public float speed;
    private Vector2 _movementInput;

    [Header("Animation")]
    private Animator _animator;

    [Header("Jump")]
    public float jumpForce;
    public LayerMask groundLayerMask;
    private Vector2 _boxCastSize = new Vector2(0.4f, 0.05f);
    private float _boxCastMaxDistance = 0.7f;

    private PlayerCollision _skill;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private CharacterStats _stats;

    private bool _isMoveInput;
    private bool _isAttack;
    private bool _isSkill;
    private float _AttackDealyTime = float.MaxValue;
    #endregion

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        //_stats = GameManager.instance.DataManager.playerStats;
        _skill = GetComponent<PlayerCollision>();
    }

    private void Start()
    {
        _stats = GameManager.instance.DataManager.PlayerCurrentStats;
    }

    private void FixedUpdate()
    {
        Move();
        HandleJumpAnimation();
        HandleAttackSpeed();
    }

    private void HandleAttackSpeed()
    {
        if(_AttackDealyTime <= _stats.attackSO.attackSpeed)
        {
            _AttackDealyTime += Time.deltaTime;
        }
        else if(_isAttack)
        {
            _animator.SetBool("IsShoot", true);
            _animator.SetBool("Jump", false);
            _animator.SetBool("Fall", false);
            Vector2 direction = (_spriteRenderer.flipX) ? Vector2.left : Vector2.right;
            BulletManager.instance.ShootBullet(gameObject.transform.position, direction, _stats.attackSO.range);
            _AttackDealyTime = 0f;
        }

        if(_isSkill)
        {
            _animator.SetBool("IsShoot", true);
            _animator.SetBool("Jump", false);
            _animator.SetBool("Fall", false);
            Vector2 direction = (_spriteRenderer.flipX) ? Vector2.left : Vector2.right;
            BulletManager.instance.ShootSkill(_skill.SkillData, gameObject.transform.position, direction);
        }
    }
    private void HandleJumpAnimation()
    {
        if (_rigidbody.velocity.y < 0 && !_isAttack && !_isSkill)
        {
            _animator.SetBool("Jump", false);
            _animator.SetBool("Fall", true);
            if (OnGround())
            {
                _animator.SetBool("Fall", false);
            }
        }
    }
    private bool OnGround()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, _boxCastSize, 0f, Vector2.down, _boxCastMaxDistance, groundLayerMask);
        return (raycastHit.collider != null);
    }
    private void Move()
    {
        if (OnGround() && _isMoveInput) _animator.SetBool("IsMove", true);
        if(!OnGround()) _animator.SetBool("IsMove", false);
        Vector2 dir = _movementInput;
        dir *= _stats.speed;
        dir.y = _rigidbody.velocity.y;

        if (dir.x > 0) _spriteRenderer.flipX = false;
        else if (dir.x < 0) _spriteRenderer.flipX = true;

        _rigidbody.velocity = dir;
    }

    #region InputSystem

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _movementInput = context.ReadValue<Vector2>();
            _isMoveInput = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _movementInput = Vector2.zero;
            _isMoveInput = false;
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
            _isAttack = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            if(!_isSkill) _animator.SetBool("IsShoot", false);
            _isAttack = false;
        }
    }

    public void OnSkillInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (_skill.SkillData == null) return;
            _isSkill = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            if(!_isAttack) _animator.SetBool("IsShoot", false);
            _isSkill = false;
        }
    }
    #endregion
}
