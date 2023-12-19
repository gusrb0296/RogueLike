using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : MonoBehaviour
{
    public PotionItemData PotionData;
    [SerializeField] private SkillManager _skillItemDataList;
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _circleCollider;

    private float _originAtk;
    private float _originSpeed;
    private float _originAttackSpeed;
    private float _changeAttackSpeed;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            UseItem();
        }
    }

    public void UseItem()
    {
        switch(PotionData.potionType)
        {
            case PotionType.Hp:
                HpItem();
                break;

            case PotionType.Power:
                PowerItem();
                break;

            case PotionType.Speed:
                SpeedItem();
                break;

            case PotionType.AttackSpeed:
                AttackSpeedItem();
                break;

            default:
                break;
        }
    }

    #region Item
    public void HpItem()
    {
        // 플레이어 체력 증가
        GameManager.instance.UpdatePlayerStatsDatas(0, (int)PotionData.Hp, 0);
        Debug.Log(GameManager.instance.DataManager.PlayerCurrentStats.currentHealth);

        Destroy(gameObject);
    }

    public void PowerItem()
    {
        _spriteRenderer.color = new Color(255, 255, 255, 0);
        _circleCollider.enabled = false;

        // 플레이어 기본 공격x 2
        _originAtk = GameManager.instance.DataManager.PlayerCurrentStats.attackSO.power;
        GameManager.instance.UpdatePlayerAttackSODatas(0, _originAtk, 0);

        // 스킬 공격x2
        foreach (SkillItemData skill in _skillItemDataList.skillDataList)
        {
            skill.Atk *= 2;
        }

        StartCoroutine(RestorePower());
    }

    IEnumerator RestorePower()
    {
        yield return new WaitForSeconds(5f);

        // 기본 공격 복구
        GameManager.instance.UpdatePlayerAttackSODatas(0, -_originAtk, 0);

        // 스킬 복구
        foreach (SkillItemData skill in _skillItemDataList.skillDataList)
        {
            skill.Atk /= 2;
        }

        Destroy(gameObject);
    }

    public void SpeedItem()
    {
        // 플레이어 속도x2
        _spriteRenderer.color = new Color(255, 255, 255, 0);
        _circleCollider.enabled = false;

        _originSpeed = GameManager.instance.DataManager.PlayerCurrentStats.speed;
        GameManager.instance.UpdatePlayerStatsDatas(0, 0, (int)_originSpeed);

        StartCoroutine(RestoreSpeed());
    }

    IEnumerator RestoreSpeed()
    {
        yield return new WaitForSeconds(5f);

        // 스피드 복구
        GameManager.instance.UpdatePlayerStatsDatas(0, 0, -(int)_originSpeed);

        Destroy(gameObject);
    }

    public void AttackSpeedItem()
    {
        _spriteRenderer.color = new Color(255, 255, 255, 0);
        _circleCollider.enabled = false;

        // 플레이어 기본 공격속도 x 2 코드
        _originAttackSpeed = GameManager.instance.DataManager.PlayerCurrentStats.attackSO.attackSpeed;
        _changeAttackSpeed = _originAttackSpeed / 2;
        GameManager.instance.UpdatePlayerAttackSODatas(-_changeAttackSpeed, 0, 0);

        // 스킬 공격속도x2
        foreach (SkillItemData skill in _skillItemDataList.skillDataList)
        {
            skill.CoolTime /= 2;
        }

        StartCoroutine(RestoreAttackSpeed());

    }

    IEnumerator RestoreAttackSpeed()
    {
        yield return new WaitForSeconds(5f);

        // 기본 공격 속도 복구
        GameManager.instance.UpdatePlayerAttackSODatas(_changeAttackSpeed, 0, 0);

        // 스킬 속도 복구
        foreach (SkillItemData skill in _skillItemDataList.skillDataList)
        {
            skill.CoolTime *= 2;
        }

        Destroy(gameObject);
    }
    #endregion
}
