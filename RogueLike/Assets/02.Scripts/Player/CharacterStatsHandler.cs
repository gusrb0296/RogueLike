using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsHandler : MonoBehaviour
{
    [SerializeField] private CharacterStats baseStats;

    public CharacterStats CurrentStats { get; private set; }

    private void Awake()
    {
        UpdateStats();
    }

    private void UpdateStats()
    {
        AttackSO attackSO = null;
        if (baseStats != null) attackSO = Instantiate(baseStats.attackSO);

        CurrentStats = new CharacterStats { attackSO = attackSO };
        CurrentStats.maxHealth = baseStats.maxHealth;
        CurrentStats.speed = baseStats.speed;
        CurrentStats.maxStamina = baseStats.maxStamina;
        CurrentStats.invincibilityTime = baseStats.invincibilityTime;

        GameManager.instance.PlayerDataUpdate(CurrentStats);
    }
}
