using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [HideInInspector]
    public DataManager dataManager;
    public StageManager stageManager;
    public AudioManager audioManager;
    public UIManager uiManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        dataManager = GetComponentInChildren<DataManager>();
        stageManager = GetComponentInChildren<StageManager>();
        audioManager = GetComponentInChildren<AudioManager>();
        uiManager = GetComponentInChildren<UIManager>();
    }

    private void Update()
    {
        
    }
}
