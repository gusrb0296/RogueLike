using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttackData", menuName = "Character/Attacks/Default")]
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")]
    public float attackSpeed;
    public float power;
    public float range;
    public LayerMask target;
}
