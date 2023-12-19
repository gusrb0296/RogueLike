using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : MonoBehaviour
{
    public PotionItemData PotionData;
    [SerializeField] private SkillItemDataList _skillItemDataList;
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _circleCollider;

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

    public void HpItem()
    {
        // 플레이어 체력 증가 코드 TODO   
        Debug.Log("플레이어 Hp " + PotionData.Hp + " 회복");

        Destroy(gameObject);
    }

    public void PowerItem()
    {
        _spriteRenderer.color = new Color(255, 255, 255, 0);
        _circleCollider.enabled = false;

        // 플레이어 기본 공격 x 2 코드 TODO


        // 스킬 공격 x 2 코드
        foreach (SkillItemData skill in _skillItemDataList.skillDataList)
        {
            skill.Atk *= 2;
        }

        StartCoroutine(RestorePower());
    }

    IEnumerator RestorePower()
    {
        yield return new WaitForSeconds(5f);
        
        // 기본 공격 복구 TODO

        // 스킬 복구
        foreach (SkillItemData skill in _skillItemDataList.skillDataList)
        {
            skill.Atk /= 2;
        }

        Destroy(gameObject);
    }

    public void SpeedItem()
    { 
        // 플레이어 속도 x 2 코드 TODO
    }

    public void AttackSpeedItem()
    {
        _spriteRenderer.color = new Color(255, 255, 255, 0);
        _circleCollider.enabled = false;

        // 플레이어 기본 공격속도 x 2 코드 TODO

        // 스킬 공격속도 x 2 코드
        foreach (SkillItemData skill in _skillItemDataList.skillDataList)
        {
            skill.CoolTime /= 2;
        }

        StartCoroutine(RestoreAttackSpeed());

    }

    IEnumerator RestoreAttackSpeed()
    {
        yield return new WaitForSeconds(5f);

        // 기본 공격 속도 복구 TODO

        // 스킬 속도 복구
        foreach (SkillItemData skill in _skillItemDataList.skillDataList)
        {
            skill.CoolTime *= 2;
        }

        Destroy(gameObject);
    }
}
