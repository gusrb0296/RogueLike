using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    //[연호] 스테이지 씬 들어가서 이벤트 호출하면 몬스터 소환하려고 합니다.
    public event Action OnStageScene;
    public void CallStageStart()
    {
        OnStageScene?.Invoke();
    }
    // 게임의 스테이지를 관리

    // 스테이지의 초기화(생성), 종료, 이동 등을 처리

    // 스테이지에 필요한 리로스를 로드/ 해제

}
