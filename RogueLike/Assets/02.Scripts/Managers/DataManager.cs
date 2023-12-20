using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    // ���� �����͸� �ε��ϰ� �����ϴ� ����

    // �÷��̾� ����, ������ ������, ���� ���� ���� �����͸� ����

    // �������� ���� �� ������Ʈ�� �ʿ��� ��� �̺�Ʈ �ý����� Ȱ��(����)


    #region Player Global Variable
    private GameObject player;

    public List<SkillItemData> SkillDataList = new List<SkillItemData>();


    public GameObject Player
    {
        get { return player; }
        set { player = value; }
    }

    // baseStats.AttackSO (attackSpeed, power, range, target)
    [SerializeField] private CharacterStats PlayerBaseStats;

    // CharacterStats.PlayerCurrentStats (maxHealth, maxStamina, speed, invincibilityTime)
    public CharacterStats PlayerCurrentStats { get; private set; }

    public int PlayerCurrentGold { get; private set; }
    #endregion

    private void Start()
    {
        // if (PlayerBaseStats == null) PlayerBaseStats.attackSO = Resources.Load("Prefabs/DefaultAttackData", typeof(ScriptableObject)) as AttackSO;
    }

    #region Player Data
    public void InitializePlayerData()
    {
        AttackSO attackSO = null;
        if (PlayerBaseStats != null)
            attackSO = Instantiate(PlayerBaseStats.attackSO);

        PlayerCurrentStats = new CharacterStats { attackSO = attackSO };
        PlayerCurrentStats.maxHealth = PlayerBaseStats.maxHealth;
        PlayerCurrentStats.currentHealth = PlayerBaseStats.currentHealth;
        PlayerCurrentStats.speed = PlayerBaseStats.speed;
        PlayerCurrentStats.maxStamina = PlayerBaseStats.maxStamina;
        PlayerCurrentStats.invincibilityTime = PlayerBaseStats.invincibilityTime;
    }

    // 1�� ��� (AttackSO�� ��°�� �Ѱ��༭ ��ü)
    public void UpdatePlayerAttackSOData(AttackSO attackSO)
    {
        CharacterStats PlayerChangeStats = new CharacterStats { attackSO = attackSO };

        PlayerCurrentStats.attackSO.attackSpeed += PlayerChangeStats.attackSO.attackSpeed;
        PlayerCurrentStats.attackSO.power += PlayerChangeStats.attackSO.power;
        PlayerCurrentStats.attackSO.range += PlayerChangeStats.attackSO.range;
    }

    // 2�� ��� (�� ���� �޾Ƽ� ��ü)
    public void UpdatePlayerAttckSOData(float atkSpeed, float power, float range)
    {
        PlayerCurrentStats.attackSO.attackSpeed += atkSpeed;
        PlayerCurrentStats.attackSO.power += power;
        PlayerCurrentStats.attackSO.range += range;
    }

    public void UpdatePlayerStatsData(int maxHealth ,int health, int speed)
    {
        PlayerCurrentStats.maxHealth += maxHealth;
        PlayerCurrentStats.currentHealth += health;
        PlayerCurrentStats.speed += speed;
    }
    public void ChangeHealth(float value)
    {
        PlayerCurrentStats.currentHealth = Mathf.Clamp(PlayerCurrentStats.currentHealth - (int)value, 0, PlayerCurrentStats.maxHealth);
        Debug.Log("�������� �Ծ����ϴ� " + value);
        Debug.Log("����ü��  " + PlayerCurrentStats.currentHealth);

        if (PlayerCurrentStats.currentHealth == 0)
        {
            Die();
        }
    }

    public void UpdatePlayerCurrentGold(int value)
    {
        PlayerCurrentGold += value;
    }

    private void Die()
    {
        Player.GetComponentInChildren<Animator>().SetBool("IsDie", true);
        GameManager.instance.UiManager.GameOverAnim();
    }
    #endregion

    #region Monster Data

    #endregion

    #region Item Data
    public void InitializeSkillData()
    {
        // Bolt
        SkillDataList[0].Atk = 10f;
        SkillDataList[0].CoolTime = 0.5f;
        SkillDataList[0].SkillSpeed = 10f;

        // CrossedWave
        SkillDataList[1].Atk = 10f;
        SkillDataList[1].CoolTime = 0.5f;
        SkillDataList[1].SkillSpeed = 10f;

        // FastSlash
        SkillDataList[2].Atk = 10f;
        SkillDataList[2].CoolTime = 0.5f;
        SkillDataList[2].SkillSpeed = 10f;

        // FireBall
        SkillDataList[3].Atk = 10f;
        SkillDataList[3].CoolTime = 0.5f;
        SkillDataList[3].SkillSpeed = 10f;

        // Laser
        SkillDataList[4].Atk = 10f;
        SkillDataList[4].CoolTime = 0.5f;
        SkillDataList[4].SkillSpeed = 10f;
    }
    #endregion
}
