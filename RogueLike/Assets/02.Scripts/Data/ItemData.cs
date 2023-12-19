using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    Effect,
    Skiil,
}


[CreateAssetMenu(fileName = "ItemData_", menuName = "Data/ItemData", order = 0)]
public class ItemData : ScriptableObject
{
    public ItemType Type;
    public string Name;
    public string Disription;
}
