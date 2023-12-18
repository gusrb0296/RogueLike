using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public bool GameIsPaused = false;
    public GameObject PuaseMenuPanel;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
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
        PuaseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause()
    {
        PuaseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
