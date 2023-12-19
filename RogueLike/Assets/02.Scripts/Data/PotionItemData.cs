using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionType
{
    None,
    Hp,
    Power,
    Speed,
    AttackSpeed,
}

[CreateAssetMenu(fileName = "PotionItemData_", menuName = "Data/PotionItemData", order = 1)]
public class PotionItemData : ItemData
{

    [Header("PotionType")]
    public PotionType potionType;

    [Header("Stats")]
    public float Hp;
    
}
