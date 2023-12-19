using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // 게임 내 UI 관리

    // 화면에 표시되는 UI의 초기화, 갠신, 숨김 등을 처리

    // 사용자 입력에 따른 이벤트 처리 담당

    public bool GameIsPaused = false;
    public GameObject PuaseMenuPanel;  

    private void Awake()
    {        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PuaseMenuPanel == null) return;

            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Resume()
    {
        UIClose(PuaseMenuPanel);
        Time.timeScale = 1f;
        Debug.Log("게임 재개");
        GameIsPaused = false;
    }

    private void Pause()
    {
        UIOpen(PuaseMenuPanel);
        Time.timeScale = 0f;
        Debug.Log("게임 정지");
        GameIsPaused = true;
    }

    private void UIOpen(GameObject wantToOpenUI)
    {
        wantToOpenUI.SetActive(true);
    }

    private void UIClose(GameObject wantToCloaseUI)
    {
        wantToCloaseUI.SetActive(false);
    }
}
