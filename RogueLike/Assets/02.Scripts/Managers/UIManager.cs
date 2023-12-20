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
    public GameObject PauseMenuPanel;

    public float num;

    [Header("UI")]
    public TextMeshProUGUI HP_txt;
    public Image HP_Bar;
    public TextMeshProUGUI CurrentGem_txt;
    public GameObject GameOverText;
    public Animator GameOverAnimation;
    public Image SkillIcon;
    private float currentHealth;
    private int currentGem;
    private int needLvUpGem;

    [Header("Stats InterFace")]
    public TextMeshProUGUI Level_stats;
    public TextMeshProUGUI HP_stats;
    public TextMeshProUGUI power_stats;
    public TextMeshProUGUI mobility_stats;
    public TextMeshProUGUI attackSpeed_stats;

    [Header("Stats Modifier")]
    public TextMeshProUGUI Gem_modifier;
    public TextMeshProUGUI HP_modifier;
    public TextMeshProUGUI power_modifier;
    public TextMeshProUGUI mobility_modifier;
    public TextMeshProUGUI attackSpeed_modifier;

    [HideInInspector]
    private float power;
    private float mobility = 0.5f;
    private float attackSpeed;
    private int Level = 1;
    private int maxHealth;

    DataManager dataManager;


    private void Start()
    {
        dataManager = GameManager.instance.DataManager;

        maxHealth = dataManager.PlayerCurrentStats.maxHealth;
        power = dataManager.PlayerCurrentStats.attackSO.power;
        attackSpeed = dataManager.PlayerCurrentStats.attackSO.attackSpeed;
        //mobility = dataManager.PlayerCurrentStats.speed;
        currentHealth = dataManager.PlayerCurrentStats.currentHealth;



        //기본 UI에 값들이 보이게 초기화, Setting to can see UI Stats
<<<<<<< HEAD
<<<<<<< HEAD
        HP_txt.text = currentHealth + " / " + maxHealth;
=======
        //HP_txt.text = currentHealth + " / " + maxHealth;
>>>>>>> parent of 5477b25 (Merge branch 'Develop' into KHK-UI_Update)
=======
        HP_txt.text = currentHealth + " / " + maxHealth;
>>>>>>> parent of 7c9dcb5 (Merge pull request #12 from gusrb0296/KHK-UI_Update)


        //Stats 설정에 값들이 보이게 설정
        Level_stats.text = Level.ToString();
        HP_stats.text = maxHealth.ToString();
        power_stats.text = power.ToString();
        //mobility_stats.text = mobility.ToString();
        attackSpeed_stats.text = attackSpeed.ToString();

        HP_Bar.type = Image.Type.Filled;

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
<<<<<<< HEAD
<<<<<<< HEAD
        //Update에서 획득시로 수정 필요함.
        CurrentGem_txt.text = dataManager.PlayerCurrentGold.ToString();
=======

        //GameOVer 이후 엔터 입력 시 게임 종료
        if (Input.GetKeyDown(KeyCode.Return) && IsGameOver == true)
        {
            ReturnToStartScene();
        }

        //Gem 획득시 UI적용. Update에서 획득시로 수정 필요함.
        CurrentGem_txt.text = dataManager.PlayerCurrentGold.ToString();

        //HP바 게이지 변동
        ChangeDisplayHealth();
>>>>>>> parent of 5477b25 (Merge branch 'Develop' into KHK-UI_Update)
=======
        //Update에서 획득시로 수정 필요함.
        CurrentGem_txt.text = dataManager.PlayerCurrentGold.ToString();
>>>>>>> parent of 7c9dcb5 (Merge pull request #12 from gusrb0296/KHK-UI_Update)
    }

    public void Resume()
    {
        UIClose(PauseMenuPanel);
        Time.timeScale = 1f;
        Debug.Log("게임 재개");
        GameIsPaused = false;
    }

    private void Pause()
    {
        GameOverAnim();
        UIOpen(PauseMenuPanel);
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

    public void ChangeDisplayHealth()
    {
        HP_Bar.fillAmount = (float)dataManager.PlayerCurrentStats.currentHealth / (float)dataManager.PlayerCurrentStats.maxHealth;
        HP_txt.text = dataManager.PlayerCurrentStats.currentHealth + " / " + maxHealth;
    }

    public void GameOverAnim()
    {
        UIOpen(GameOverText);
        GameOverAnimation.SetTrigger("GameOver");

        Invoke("TimeScaleZero", 1.5f);
    }

<<<<<<< HEAD
    public void GameClearAnim()
    {
        UIOpen(GameClearPanel);
    }

    private void TimeScaleZero()
    {
        Time.timeScale = 0f;
    }

    
<<<<<<< HEAD
=======
    private void GameOVer()
    {
        //Time.timeScale = 0f;
        IsGameOver = true;
    }

    public void ReturnToStartScene()
    {
        //Time.timeScale = 1f;
        IsGameOver = false;
        //Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
        UIClose(MainUI);
        UIClose(GameOverText);
    }

    public void MainUIActive()
    {
        UIOpen(MainUI);
    }
>>>>>>> parent of 5477b25 (Merge branch 'Develop' into KHK-UI_Update)
=======
>>>>>>> parent of 7c9dcb5 (Merge pull request #12 from gusrb0296/KHK-UI_Update)
}
