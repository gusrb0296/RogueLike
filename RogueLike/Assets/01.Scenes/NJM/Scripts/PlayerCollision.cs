using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [HideInInspector] public SkillItemData SkillData;
    private bool _isInvincibility;

    private Animator _animator;
    private CharacterStatsHandler _stats;
    private SpriteRenderer _spriteRenderer;

    private int _invincibleEffectCycle = 5;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _stats = GetComponent<CharacterStatsHandler>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        SkillData = null;
        _isInvincibility = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // SkillItem 일 경우
        if (collision.gameObject.GetComponent<SkillItem>() && collision.gameObject.GetComponent<SkillItem>().SkillData.Type == ItemType.Skiil)
        {
            SkillData = collision.gameObject.GetComponent<SkillItem>().SkillData;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (_isInvincibility) return;
            _animator.SetTrigger("Hurt");
            _isInvincibility = true;
            Debug.Log("적에게 피격 당함");
            StartCoroutine(HandleInvincibilityTime());
        }
    }

    IEnumerator HandleInvincibilityTime()
    {
        //for (int i = 0; i < _invincibleEffectCycle; i++)
        //{
        //    _spriteRenderer.color = new Color32(255,255,255,90);
        //    yield return new WaitForSeconds(_stats.CurrentStats.invincibilityTime / (_invincibleEffectCycle * 2));
        //    _spriteRenderer.color = new Color32(255,255,255,180);
        //    yield return new WaitForSeconds(_stats.CurrentStats.invincibilityTime / (_invincibleEffectCycle * 2));
        //}
        yield return new WaitForSeconds(_stats.CurrentStats.invincibilityTime);
        _isInvincibility = false;
    }
}
