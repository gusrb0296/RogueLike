using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    // 게임 데이터를 로드하고 저장하는 역할

    // 플레이어 정보, 아이템 데이터, 게임 설정 등의 데이터를 관리

    // 데이터의 변경 및 업데이트가 필요한 경우 이벤트 시스템을 활용(구독)

    public List<GameObject> skillItemPrefabs { get; private set; } = new List<GameObject>();


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

    // 1번 방법 (AttackSO를 통째로 넘겨줘서 교체)
    public void UpdatePlayerAttackSOData(AttackSO attackSO)
    {
        CharacterStats PlayerChangeStats = new CharacterStats { attackSO = attackSO };

        PlayerCurrentStats.attackSO.attackSpeed += PlayerChangeStats.attackSO.attackSpeed;
        PlayerCurrentStats.attackSO.power += PlayerChangeStats.attackSO.power;
        PlayerCurrentStats.attackSO.range += PlayerChangeStats.attackSO.range;
    }

    // 2번 방법 (각 값을 받아서 교체)
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
        Debug.Log("데미지를 입었습니다 " + value);
        Debug.Log("현재체력  " + PlayerCurrentStats.currentHealth);
        GameManager.instance.UiManager.ChangeDisplayHealth();

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
        SkillDataList[0].Atk = GameManager.instance.DataManager.PlayerCurrentStats.attackSO.power * 4f;
        SkillDataList[0].CoolTime = 4f;
        SkillDataList[0].SkillSpeed = 10f;

        // CrossedWave
        SkillDataList[1].Atk = GameManager.instance.DataManager.PlayerCurrentStats.attackSO.power * 3f;
        SkillDataList[1].CoolTime = 3f;
        SkillDataList[1].SkillSpeed = 10f;

        // FastSlash
        SkillDataList[2].Atk = GameManager.instance.DataManager.PlayerCurrentStats.attackSO.power * 1.5f;
        SkillDataList[2].CoolTime = 1.5f;
        SkillDataList[2].SkillSpeed = 20f;

        // FireBall
        SkillDataList[3].Atk = GameManager.instance.DataManager.PlayerCurrentStats.attackSO.power * 7f;
        SkillDataList[3].CoolTime = 5f;
        SkillDataList[3].SkillSpeed = 5f;

        // Laser
        SkillDataList[4].Atk = GameManager.instance.DataManager.PlayerCurrentStats.attackSO.power * 2f;
        SkillDataList[4].CoolTime = 1.5f;
        SkillDataList[4].SkillSpeed = 15f;
    }

    public void LoadSkillPrefab()
    {
        skillItemPrefabs = Resources.LoadAll<GameObject>("Prefabs\\SkillItem").ToList();
    }
    #endregion
}
