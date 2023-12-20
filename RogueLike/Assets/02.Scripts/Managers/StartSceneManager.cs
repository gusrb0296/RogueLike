using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    //public GameObject UI;

    private void Awake()
    {
    }

    public void ToMainScene()
    {        
        SceneManager.LoadScene("MainScene_KHK");
        GameManager.instance.UiManager.MainUIActive(); // �� �Ѿ �� UI Ȱ��ȭ
        GameManager.instance.PlayerDataInitialiez(); // ������ �ʱ�ȭ

        //SceneManager.LoadSceneAsync("MainScene_KHK", LoadSceneMode.Additive).completed += OnSceneLoaded;
        //SceneManager.LoadScene("MainScene_KHK", LoadSceneMode.Additive);
        //InstantiateIOObject();
        //UI.SetActive(true);
    }

    /*
    private void OnSceneLoaded(AsyncOperation operation)
    {
        if(operation.isDone)
        {
            InstantiateIOObject();
        }
    }
    
    private void InstantiateIOObject()
    {
        GameObject UIPrefab = Resources.Load<GameObject>("UI");
        Instantiate(UIPrefab);
    }
    */
    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
