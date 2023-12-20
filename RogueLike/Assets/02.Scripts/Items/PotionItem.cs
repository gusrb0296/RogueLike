using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : MonoBehaviour
{
    public PotionItemData PotionData;
    
    private SpriteRenderer _spriteRenderer;
    private ColorChange _colorChange;
    private CircleCollider2D _circleCollider;
    private float _originAtk;
    private float _originSpeed;
    private float _originAttackSpeed;
    private float _changeAttackSpeed;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _colorChange = GetComponent<ColorChange>();
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
        GameManager.instance.AudioManager.SFX(PotionData.ItemSound);

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
    
    // Hp 회복
    public void HpItem()
    {
        // 플레이어 체력 증가
        GameManager.instance.UpdatePlayerStatsDatas(0, (int)PotionData.Hp, 0);
        Debug.Log(GameManager.instance.DataManager.PlayerCurrentStats.currentHealth);

        Destroy(gameObject);
    }

    // 5초 동안, 기본공격, 스킬 데미지 두배
    public void PowerItem()
    {
        _spriteRenderer.color = new Color(255, 255, 255, 0);
        _circleCollider.enabled = false;
        _colorChange.PlayerColorChange(new Color(255 / 255f, 104 / 255f, 76 / 255f, 255 / 255f));

        // 기본 공격
        _originAtk = GameManager.instance.DataManager.PlayerCurrentStats.attackSO.power;
        GameManager.instance.UpdatePlayerAttackSODatas(0, _originAtk, 0);

        // 스킬 공격
        foreach (SkillItemData skill in GameManager.instance.DataManager.SkillDataList)
        {
            skill.Atk *= 2;
        }

        StartCoroutine(RestorePower());
    }

    // 데미지 복구
    IEnumerator RestorePower()
    {
        yield return new WaitForSeconds(5f);

        // 기본 공격
        GameManager.instance.UpdatePlayerAttackSODatas(0, -_originAtk, 0);

        // 스킬
        foreach (SkillItemData skill in GameManager.instance.DataManager.SkillDataList)
        {
            skill.Atk /= 2;
        }

        Destroy(gameObject);
    }
    
    // 5초 동안, 스피드 두배
    public void SpeedItem()
    {
        _spriteRenderer.color = new Color(255, 255, 255, 0);
        _circleCollider.enabled = false;
        _colorChange.PlayerColorChange(new Color(86 / 255f, 106 / 255f, 255 / 255f, 255 / 255f));

        _originSpeed = GameManager.instance.DataManager.PlayerCurrentStats.speed;
        GameManager.instance.UpdatePlayerStatsDatas(0, 0, (int)_originSpeed);

        StartCoroutine(RestoreSpeed());
    }

    // 스피드 복구
    IEnumerator RestoreSpeed()
    {
        yield return new WaitForSeconds(5f);

        GameManager.instance.UpdatePlayerStatsDatas(0, 0, -(int)_originSpeed);

        Destroy(gameObject);
    }

    // 5초 동안, 기본공격, 스킬 속도 두배
    public void AttackSpeedItem()
    {
        _spriteRenderer.color = new Color(255, 255, 255, 0);
        _circleCollider.enabled = false;
        _colorChange.PlayerColorChange(new Color(118 / 255f, 255 / 255f, 98 / 255f, 255 / 255f));

        // 기본 공격
        _originAttackSpeed = GameManager.instance.DataManager.PlayerCurrentStats.attackSO.attackSpeed;
        _changeAttackSpeed = _originAttackSpeed / 2;
        GameManager.instance.UpdatePlayerAttackSODatas(-_changeAttackSpeed, 0, 0);

        // 스킬
        foreach (SkillItemData skill in GameManager.instance.DataManager.SkillDataList)
        {
            skill.CoolTime /= 2;
        }

        StartCoroutine(RestoreAttackSpeed());

    }

    // 공격 속도 복구
    IEnumerator RestoreAttackSpeed()
    {
        yield return new WaitForSeconds(5f);

        // 기본 공격 속도 복구
        GameManager.instance.UpdatePlayerAttackSODatas(_changeAttackSpeed, 0, 0);

        // 스킬 속도 복구
        foreach (SkillItemData skill in GameManager.instance.DataManager.SkillDataList)
        {
            skill.CoolTime *= 2;
        }

        Destroy(gameObject);
    }
    #endregion
}
