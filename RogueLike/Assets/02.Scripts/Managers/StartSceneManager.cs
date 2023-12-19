using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{

    public GameObject UI;
    
    public void ToMainScene()
    {
        SceneManager.LoadScene("MainScene_KHK");
        UI.SetActive(true);
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
