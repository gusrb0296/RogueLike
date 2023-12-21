using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterStats
{
     public int maxHealth;
    [Range(0, 100)] public int currentHealth;
    [Range(0, 100)] public int maxStamina;
    [Range(1f, 10f)] public float speed;
    public float invincibilityTime;

    public AttackSO attackSO;
}
