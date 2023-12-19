using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // 게임 데이터를 로드하고 저장하는 역할

    // 플레이어 정보, 아이템 데이터, 게임 설정 등의 데이터를 관리

    // 데이터의 변경 및 업데이트가 필요한 경우 이벤트 시스템을 활용(구독)


    #region Player Global Variable
    // baseStats.AttackSO (attackSpeed, power, range, target)
    [SerializeField] public CharacterStats PlayerBaseStats { get; private set; }

    // CharacterStats.PlayerCurrentStats (maxHealth, maxStamina, speed, invincibilityTime)
    public CharacterStats PlayerCurrentStats { get; private set; }
    #endregion

    private void Start()
    {
        // if (PlayerBaseStats == null) PlayerBaseStats = Resources.Load("Prefabs/DefaultAttackData", typeof(ScriptableObject)) as CharacterStats;
    }

    #region Player Data
    public void InitializePlayerData()
    {
        AttackSO attackSO = null;
        if (PlayerBaseStats != null)
            attackSO = Instantiate(PlayerBaseStats.attackSO);

        PlayerCurrentStats = new CharacterStats { attackSO = attackSO };
        PlayerCurrentStats.maxHealth = PlayerBaseStats.maxHealth;
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

    public void UpdatePlayerStatsData(int health, int speed)
    {
        PlayerCurrentStats.maxHealth += health;
        PlayerCurrentStats.speed += speed;
    }
    #endregion

    #region Monster Data

    #endregion

    #region Item Data

    #endregion
}
