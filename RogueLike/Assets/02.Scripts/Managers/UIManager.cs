using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // ���� �� UI ����

    // ȭ�鿡 ǥ�õǴ� UI�� �ʱ�ȭ, ����, ���� ���� ó��

    // ����� �Է¿� ���� �̺�Ʈ ó�� ���

    public bool GameIsPaused = false;
    public GameObject PauseMenuPanel;

    public float num;

    [Header("UI")]
    public TextMeshProUGUI HP_txt;
    public Image HP_Bar;
    public TextMeshProUGUI CurrentGem_txt;
    public GameObject GameOverText;
    public GameObject GameClearPanel;
    public Animator GameOverAnimation;
    public Image SkillIcon;

    //private float currentHealth;
    //private int currentGem;
    //private int needLvUpGem;

    //[Header("Stats InterFace")]
    //public TextMeshProUGUI Level_stats;
    //public TextMeshProUGUI HP_stats;
    //public TextMeshProUGUI power_stats;
    //public TextMeshProUGUI mobility_stats;
    //public TextMeshProUGUI attackSpeed_stats;

    //[Header("Stats Modifier")]
    //public TextMeshProUGUI Gem_modifier;
    //public TextMeshProUGUI HP_modifier;
    //public TextMeshProUGUI power_modifier;
    //public TextMeshProUGUI mobility_modifier;
    //public TextMeshProUGUI attackSpeed_modifier;

    //[HideInInspector]
    //private float power;
    //private float mobility = 0.5f;
    //private float attackSpeed;
    //private int Level = 1;
    //private int maxHealth;

    DataManager dataManager;
    private bool IsGameOver;
    public GameObject MainUI;
    private Sprite basicSkillIcon;
    public Image disable;

    public GameObject statsWindow;
    private void Start()
    {
        dataManager = GameManager.instance.DataManager;

        //maxHealth = dataManager.PlayerCurrentStats.maxHealth;
        //power = dataManager.PlayerCurrentStats.attackSO.power;
        //attackSpeed = dataManager.PlayerCurrentStats.attackSO.attackSpeed;
        ////mobility = dataManager.PlayerCurrentStats.speed;
        //currentHealth = dataManager.PlayerCurrentStats.currentHealth;



        ////�⺻ UI�� ������ ���̰� �ʱ�ȭ, Setting to can see UI Stats
        ////HP_txt.text = currentHealth + " / " + maxHealth;


        ////Stats ������ ������ ���̰� ����
        //Level_stats.text = Level.ToString();
        //HP_stats.text = maxHealth.ToString();
        //power_stats.text = power.ToString();
        ////mobility_stats.text = mobility.ToString();
        //attackSpeed_stats.text = attackSpeed.ToString();

        HP_Bar.type = Image.Type.Filled;
        basicSkillIcon = SkillIcon.sprite;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseMenuPanel == null) return;

            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) && IsGameOver == true)
        {
            ReturnToStartScene();
        }

        //Update���� ȹ��÷� ���� �ʿ���.
        CurrentGem_txt.text = dataManager.PlayerCurrentGold.ToString();

        //HP�� ������ ����
        ChangeDisplayHealth();
    }

    public void Resume()
    {
        UIClose(PauseMenuPanel);
        Time.timeScale = 1f;
        Debug.Log("���� �簳");
        GameIsPaused = false;
    }

    private void Pause()
    {
        UIOpen(PauseMenuPanel);
        Time.timeScale = 0f;
        Debug.Log("���� ����");
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

    public void ChangeDisplayHealth()
    {
        HP_Bar.fillAmount = (float)dataManager.PlayerCurrentStats.currentHealth / (float)dataManager.PlayerCurrentStats.maxHealth;
        HP_txt.text = dataManager.PlayerCurrentStats.currentHealth + " / " + dataManager.PlayerCurrentStats.maxHealth;
    }

    public void GameOverAnim()
    {
        UIOpen(GameOverText);
        GameOverAnimation.SetTrigger("GameOver");

        Invoke("GameOver", 1.5f);
    }

    public void GameClearAnim()
    {
        UIOpen(GameClearPanel);
    }

    private void GameOver()
    {
        IsGameOver = true;
    }

    public void ReturnToStartScene()
    {
        IsGameOver = false;
        //Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("StartScene");
        UIClose(MainUI);
        UIClose(GameOverText);

        GameManager.instance.AudioManager.BGM("startSceneBGM");
        Resume();
        SkillIcon.sprite = basicSkillIcon;
    }

    public void MainUIActive()
    {
        UIOpen(MainUI);
        UIClose(GameClearPanel);
    }
}
