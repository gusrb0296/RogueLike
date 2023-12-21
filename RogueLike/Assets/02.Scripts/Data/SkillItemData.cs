using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillItemData_", menuName = "Data/SkillItemData", order = 2)]
public class SkillItemData : ItemData
{
    [Header("Stats")]
    public float Atk;
    public float CoolTime;
    public float SkillSpeed;

    [Header("Prefabs")]
    public GameObject SkillPrefab;
    public GameObject SkillEffect;
    public GameObject SkillMissEffect;

    [Header("Resources")]
    public Sprite SkillIcon;
    public AudioClip SkillSound;
    public AudioClip SkillDamagedSound;

    [Header("ETC")]
    public Vector3 SkillPosition;
}
