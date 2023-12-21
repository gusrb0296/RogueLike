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
        GameManager.instance.UiManager.MainUIActive(); // �� �Ѿ �� UI Ȱ��ȭ
        GameManager.instance.PlayerDataInitialiez(); // ������ �ʱ�ȭ

    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
