using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{

    public GameObject UI;
    public TextMeshProUGUI speed;
    
    public void ToMainScene()
    {
        SceneManager.LoadScene("MainScene_KHK");
        UI.SetActive(true);
        speed.text = GameManager.instance.DataManager.PlayerCurrentStats.speed.ToString();
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
