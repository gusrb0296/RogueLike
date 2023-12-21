using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour, IDamagable
{
    [HideInInspector] public SkillItemData SkillData;
    private bool _isInvincibility;
    private bool _isDie;

    private Animator _animator;
    private CharacterStats _stats;
    private SpriteRenderer _spriteRenderer;

    private int _invincibleEffectCycle = 10;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _stats = GameManager.instance.DataManager.PlayerCurrentStats;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        SkillData = null;
        _isInvincibility = false;
        _isDie = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // SkillItem 일 경우
        if (collision.gameObject.GetComponent<SkillItem>() && collision.gameObject.GetComponent<SkillItem>().SkillData.Type == ItemType.Skiil)
        {
            SkillData = collision.gameObject.GetComponent<SkillItem>().SkillData;
        }

    }

    private void ReceiveDamage(float damage)
    {
        if (_isInvincibility) return;
        if (_isDie) return;

        GameManager.instance.DataManager.ChangeHealth(damage);

        _animator.SetTrigger("Hurt");
        _isInvincibility = true;
        Debug.Log("적에게 피격 당함");
        GameManager.instance.AudioManager.SFX("playerHit");
        StartCoroutine(HandleInvincibilityTime());
    }

    IEnumerator HandleInvincibilityTime()
    {
        for (int i = 0; i < _invincibleEffectCycle; i++)
        {
            _spriteRenderer.color = new Color32(150, 150, 150, 255);
            yield return new WaitForSeconds(_stats.invincibilityTime / (_invincibleEffectCycle * 2));
            _spriteRenderer.color = new Color32(255, 255, 255, 255);
            yield return new WaitForSeconds(_stats.invincibilityTime / (_invincibleEffectCycle * 2));
        }
        _isInvincibility = false;
    }

    public void TakeDamage(float damage)
    {
        ReceiveDamage(damage);
    }

    public void ChangeIsDie(bool active)
    {
        _isDie = active;
    }
}
