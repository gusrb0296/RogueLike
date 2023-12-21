using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public void ToMainScene()
    {
        SceneManager.LoadScene("MainScene_KHK");
        GameManager.instance.UiManager.MainUIActive(); // 씬 넘어갈 때 UI 활성화
        GameManager.instance.PlayerDataInitialiez(); // 데이터 초기화

    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
