using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // 게임 데이터를 로드하고 저장하는 역할

    // 플레이어 정보, 아이템 데이터, 게임 설정 등의 데이터를 관리

    // 데이터의 변경 및 업데이트가 필요한 경우 이벤트 시스템을 활용(구독)

    public CharacterStats playerStats;
    private GameObject player;

    public GameObject Player
    {
        get { return player; }
        set { player = value; }
    }

    #region Player Data
    public void UpdatePlayerData(CharacterStats stats)
    {
        // 플레이어 정보 업데이트 로직 작성 (위 파라미터 추가 필요)
        playerStats = stats;
    }

    public void ChangeHealth(float value)
    {
        playerStats.currentHealth = Mathf.Clamp(playerStats.currentHealth - (int)value, 0, playerStats.maxHealth);
        Debug.Log("데미지를 입었습니다 " + playerStats.currentHealth);
        Debug.Log("현재체력  " + playerStats.currentHealth);
    }
    #endregion

    #region Monster Data

    #endregion

    #region Item Data

    #endregion
}
