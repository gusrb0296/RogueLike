using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillItemData_", menuName = "Data/SkillItemData", order = 2)]
public class SkillItemData : ItemData
{
    public float Atk;
    public float Stamina;
    public float CoolTime;
    public GameObject SkillEffect;
    public float SkillSpeed;
    public SpringJoint SkillIcon;
}
