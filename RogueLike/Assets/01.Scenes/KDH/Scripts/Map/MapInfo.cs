using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMapType
{
    Battle,
    Reward,
    Clear
}

public enum EMoveType
{
    LeftAndRight,
    LeftAndRightAndDown,
    LeftAndRightAndUp
}

public class MapInfo : MonoBehaviour
{
    private EMapType mapType;
    private EMoveType moveType;
    private bool IsClear;
}
