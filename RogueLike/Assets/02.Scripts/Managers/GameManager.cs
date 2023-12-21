using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    #region Global Variable
    [HideInInspector]
    public DataManager DataManager { get; private set; }
    [HideInInspector]
    public StageManager StageManager { get; private set; }
    [HideInInspector]
    public AudioManager AudioManager { get; private set; }
    [HideInInspector]
    public UIManager UiManager { get; private set; }
    #endregion

    #region Data Variable
    // Player Data
    // Monster, Item ���
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);


        InitializSetting();
        AudioManager.BGM("startSceneBGM");
    }

    #region Initialize
    private void InitializSetting()
    {
        InitializeManager();
        InitializeData();
        InitializeStage();
        InitializeUI();
        InitializeAudio();
        DataManager.InitializeSkillData();
        DataManager.LoadSkillPrefab();
    }
    private void InitializeManager()
    {
        DataManager = GetComponentInChildren<DataManager>();
        StageManager = GetComponentInChildren<StageManager>();
        AudioManager = GetComponentInChildren<AudioManager>();
        UiManager = GetComponentInChildren<UIManager>();
    }

    private void InitializeData()
    {
        // ������ �ʱ�ȭ
        // �÷��̾� ������ �ʱ�ȭ
        PlayerDataInitialiez();
    }

    private void InitializeStage()
    {
        // �������� �ʱ�ȭ
    }

    private void InitializeUI()
    {
        // UI �ʱ�ȭ
    }

    private void InitializeAudio()
    {
        // ����� �ʱ�ȭ
        AudioManager.InitalizeAudios();
    }
    #endregion

    #region Data
    public void PlayerDataInitialiez()
    {
        DataManager.InitializePlayerData();
    }

    public void UpdatePlayerAttackSODatas(AttackSO attackSO)
    {
        DataManager.UpdatePlayerAttackSOData(attackSO);
    }

    public void UpdatePlayerAttackSODatas(float atkSpeed, float power, float range)
    {
        DataManager.UpdatePlayerAttckSOData(atkSpeed, power, range);
    }
    public void UpdatePlayerStatsDatas(int maxHealth,int health, float speed)
    {
        DataManager.UpdatePlayerStatsData(maxHealth ,health, speed);
    }

    public void UpdatePlayerCurrentGolds(int value)
    {
        DataManager.UpdatePlayerCurrentGold(value);
    }
    #endregion

    #region Stage

    #endregion

    #region UI

    #endregion

    #region Audio
    public void PlayBackGroundMusic(string musicName)
    {

    }

    public void PlayerSoundEffects(string soundName)
    {

    }
    #endregion
}
