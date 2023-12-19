using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    Potion,
    Skiil,
}


[CreateAssetMenu(fileName = "ItemData_", menuName = "Data/ItemData", order = 0)]
public class ItemData : ScriptableObject
{
    [Header("Commons")]
    public ItemType Type;
    public string Name;
    public string Disription;
    public AudioClip ItemSound;
}
