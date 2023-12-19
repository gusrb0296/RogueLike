using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // 게임의 스테이지를 관리

    // 스테이지의 초기화(생성), 종료, 이동 등을 처리

    // 스테이지에 필요한 리로스를 로드/ 해제

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
        if (TransitionAction != null)
            TransitionAction.Invoke(pos);
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
