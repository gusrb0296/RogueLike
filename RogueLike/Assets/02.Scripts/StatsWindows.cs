using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsWindows : MonoBehaviour
{
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
    private int Gem_modifierCount = 0;
    private float hp_modifierCount = 0;
    private float power_modifierCount = 0;
    private float mobility_modifierCount = 0.0f;
    private float attackSpeed_modifierCount = 0;

    [HideInInspector]
    private float power;
    private float mobility = 0.5f;
    private float attackSpeed;
    private int Level = 1;
    private int maxHealth;

    DataManager dataManager;

    private int statsUpPrice = 50;

    private void Awake()
    {
        dataManager = GameManager.instance.DataManager;

        UpdatePlayerStats();

        // Test
        GameManager.instance.UpdatePlayerCurrentGolds(1000);

        //Stats 설정에 값들이 보이게 설정
        UpdateStatsWindow();
    }

    private void OnEnable()
    {
        if (dataManager != null)
        {
            Gem_modifierCount = dataManager.PlayerCurrentGold;
            UpdateStatsWindow();
        }
    }

    #region Stat Text Update
    private void UpdatePlayerStats()
    {
        maxHealth = dataManager.PlayerCurrentStats.maxHealth;
        power = dataManager.PlayerCurrentStats.attackSO.power;
        attackSpeed = dataManager.PlayerCurrentStats.attackSO.attackSpeed;
        mobility = dataManager.PlayerCurrentStats.speed;
    }

    private void UpdateStatsWindow()
    {
        Level_stats.text = Level.ToString();
        HP_stats.text = maxHealth.ToString() + $"+({hp_modifierCount * 4})";
        power_stats.text = power.ToString() + $"+({power_modifierCount * 3})";
        mobility_stats.text = mobility.ToString() + $"+({mobility_modifierCount * 0.1})";
        attackSpeed_stats.text = attackSpeed.ToString() + $"-({attackSpeed_modifierCount * 0.02})";
        Gem_modifier.text = Gem_modifierCount.ToString();
    }

    private void UpdateStatsModifier()
    {
        HP_modifier.text = hp_modifierCount.ToString();
        power_modifier.text = power_modifierCount.ToString();
        mobility_modifier.text = mobility_modifierCount.ToString();
        attackSpeed_modifier.text = attackSpeed_modifierCount.ToString();
        Gem_modifier.text = Gem_modifierCount.ToString();
    }
    #endregion

    #region Gem Check
    private bool StatsUpGoldCheck()
    {
        if (Gem_modifierCount <= statsUpPrice)
            return false;

        Gem_modifierCount -= statsUpPrice;
        statsUpPrice += 10;
        return true;
    }
    private void StatsDownGoldCheck()
    {
        statsUpPrice -= 10;
        Gem_modifierCount += statsUpPrice;
    }
    #endregion

    #region ResetModifier
    private void ResetModifierCount()
    {
        hp_modifierCount = 0;
        power_modifierCount = 0;
        mobility_modifierCount = 0;
        attackSpeed_modifierCount = 0;
    }
    #endregion

    #region Stats Apply&Cancel Button
    public void StatsApplyButton()
    {
        GameManager.instance.UpdatePlayerAttackSODatas(-attackSpeed_modifierCount * 0.02f, power_modifierCount * 3, 0);
        GameManager.instance.UpdatePlayerStatsDatas((int)hp_modifierCount * 4, (int)hp_modifierCount * 4, mobility_modifierCount * 0.1f);
        GameManager.instance.UpdatePlayerCurrentGolds(-(dataManager.PlayerCurrentGold - Gem_modifierCount));
        ResetModifierCount();
        UpdatePlayerStats();
        UpdateStatsWindow();
        UpdateStatsModifier();
        GameManager.instance.PlayerSoundEffects("buttonClick");
    }

    public void StatsCancelButton()
    {
        ResetModifierCount();
        UpdatePlayerStats();
        UpdateStatsWindow();
        UpdateStatsModifier();
        GameManager.instance.PlayerSoundEffects("buttonClick");
    }
    #endregion

    #region Player Stats Up&Down Button
    // 리팩토링 예정입니다.
    public void ModifierCountUp(int index)
    {
        if (!StatsUpGoldCheck()) return;
        switch (index)
        {
            case 0:
                hp_modifierCount++;
                break;
            case 1:
                power_modifierCount++;
                break;
            case 2:
                mobility_modifierCount++;
                break;
            case 3:
                attackSpeed_modifierCount++;
                break;
        }
        UpdateStatsModifier();
        UpdateStatsWindow();
        GameManager.instance.PlayerSoundEffects("buttonClick");
    }

    public void ModifierCountDown(int index)
    {

        switch (index)
        {
            case 0:
                if (hp_modifierCount <= 0) return;
                StatsDownGoldCheck();
                hp_modifierCount--;
                break;
            case 1:
                if (power_modifierCount <= 0) return;
                StatsDownGoldCheck();
                power_modifierCount--;
                break;
            case 2:
                if (mobility_modifierCount <= 0) return;
                StatsDownGoldCheck();
                mobility_modifierCount--;
                break;
            case 3:
                if (attackSpeed_modifierCount <= 0) return;
                StatsDownGoldCheck();
                attackSpeed_modifierCount--;
                break;
        }
        UpdateStatsModifier();
        UpdateStatsWindow();
        GameManager.instance.PlayerSoundEffects("buttonClick");
    }
    #endregion
}
