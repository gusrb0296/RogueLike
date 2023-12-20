using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // ������ ���������� ����

    // ���������� �ʱ�ȭ(����), ����, �̵� ���� ó��

    // ���������� �ʿ��� ���ν��� �ε�/ ����

    public event Action<Vector3Int> TransitionAction;

    public MapCreator MapCreator { get; private set; }
    public Vector3Int CurPlyerPos { get; private set; }

    public void InitializeStage()
    {
        MapCreator = null;
        TransitionAction = null;
        CurPlyerPos = Vector3Int.zero;
    }

    public void SetStageInfo(MapCreator mapCreator, int width, int height)
    {
        MapCreator = mapCreator;
        CurPlyerPos = new Vector3Int(width / 2, height / 2);

        TransitionAction += SetPlayerPos;
        TransitionAction += RoomAction;
    }

    public Transform GetRoomTramsform(Vector3Int position)
    {
        return MapCreator.GetRoomTramsform(position);
    }

    public void Transition(Vector3Int pos)
    {
        //if (TransitionAction != null)
        //    TransitionAction.Invoke(pos);
        TransitionAction?.Invoke(pos);
    }

    private void SetPlayerPos(Vector3Int pos)
    {
        CurPlyerPos = pos;
    }

    private void RoomAction(Vector3Int pos)
    {
        RoomInfo info = MapCreator.GetRoomInfo(pos);
        info.Owner.RoomAction();
    }
}
