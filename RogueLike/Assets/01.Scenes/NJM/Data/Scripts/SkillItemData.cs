using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillItemData_", menuName = "Data/SkillItemData", order = 2)]
public class SkillItemData : ItemData
{
    [Header("Stats")]
    public float Atk;
    public float Stamina;
    public float CoolTime;
    public float SkillSpeed;

    [Header("Prefabs")]
    public GameObject SkillPrefab;
    public GameObject SkillEffect;
    public GameObject SkillMissEffect;
    public Sprite SkillIcon;

    [Header("ETC")]
    public Vector3 SkillEffectPosition;

}
