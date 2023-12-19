using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectItemData_", menuName = "Data/EffectItemData", order = 1)]
public class EffectItemData : ItemData
{
    [Header("Stats")]
    public float Hp;
    public float Stamina;
}