using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : MonoBehaviour
{
    public PotionItemData PotionData;
    //[SerializeField] private SkillManager _skillItemDataList;
    
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _circleCollider;
    private ColorChange _colorChange;
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
    
    // Hp ȸ��
    public void HpItem()
    {
        // �÷��̾� ü�� ����
        GameManager.instance.UpdatePlayerStatsDatas(0, (int)PotionData.Hp, 0);
        Debug.Log(GameManager.instance.DataManager.PlayerCurrentStats.currentHealth);

        Destroy(gameObject);
    }

    // 5�� ����, �⺻����, ��ų ������ �ι�
    public void PowerItem()
    {
        _spriteRenderer.color = new Color(255, 255, 255, 0);
        _circleCollider.enabled = false;
        _colorChange.PlayerColorChange(new Color(255 / 255f, 104 / 255f, 76 / 255f, 255 / 255f));

        // �⺻ ����
        _originAtk = GameManager.instance.DataManager.PlayerCurrentStats.attackSO.power;
        GameManager.instance.UpdatePlayerAttackSODatas(0, _originAtk, 0);

        // ��ų ����
        foreach (SkillItemData skill in GameManager.instance.DataManager.SkillDataList)
        {
            skill.Atk *= 2;
        }

        StartCoroutine(RestorePower());
    }

    // ������ ����
    IEnumerator RestorePower()
    {
        yield return new WaitForSeconds(5f);

        // �⺻ ����
        GameManager.instance.UpdatePlayerAttackSODatas(0, -_originAtk, 0);

        // ��ų
        foreach (SkillItemData skill in GameManager.instance.DataManager.SkillDataList)
        {
            skill.Atk /= 2;
        }

        Destroy(gameObject);
    }
    
    // 5�� ����, ���ǵ� �ι�
    public void SpeedItem()
    {
        _spriteRenderer.color = new Color(255, 255, 255, 0);
        _circleCollider.enabled = false;
        _colorChange.PlayerColorChange(new Color(86 / 255f, 106 / 255f, 255 / 255f, 255 / 255f));

        _originSpeed = GameManager.instance.DataManager.PlayerCurrentStats.speed;
        GameManager.instance.UpdatePlayerStatsDatas(0, 0, (int)_originSpeed);

        StartCoroutine(RestoreSpeed());
    }

    // ���ǵ� ����
    IEnumerator RestoreSpeed()
    {
        yield return new WaitForSeconds(5f);

        GameManager.instance.UpdatePlayerStatsDatas(0, 0, -(int)_originSpeed);

        Destroy(gameObject);
    }

    // 5�� ����, �⺻����, ��ų �ӵ� �ι�
    public void AttackSpeedItem()
    {
        _spriteRenderer.color = new Color(255, 255, 255, 0);
        _circleCollider.enabled = false;
        _colorChange.PlayerColorChange(new Color(118 / 255f, 255 / 255f, 98 / 255f, 255 / 255f));

        // �⺻ ����
        _originAttackSpeed = GameManager.instance.DataManager.PlayerCurrentStats.attackSO.attackSpeed;
        _changeAttackSpeed = _originAttackSpeed / 2;
        GameManager.instance.UpdatePlayerAttackSODatas(-_changeAttackSpeed, 0, 0);

        // ��ų
        foreach (SkillItemData skill in GameManager.instance.DataManager.SkillDataList)
        {
            skill.CoolTime /= 2;
        }

        StartCoroutine(RestoreAttackSpeed());

    }

    // ���� �ӵ� ����
    IEnumerator RestoreAttackSpeed()
    {
        yield return new WaitForSeconds(5f);

        // �⺻ ���� �ӵ� ����
        GameManager.instance.UpdatePlayerAttackSODatas(_changeAttackSpeed, 0, 0);

        // ��ų �ӵ� ����
        foreach (SkillItemData skill in GameManager.instance.DataManager.SkillDataList)
        {
            skill.CoolTime *= 2;
        }

        Destroy(gameObject);
    }
    #endregion
}
