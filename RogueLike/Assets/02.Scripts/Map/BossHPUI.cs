using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHPUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private Image HpBar;

    private BossMonster bossData;
    private GameObject _target;
    private float Max = 500f;

    private void Update()
    {
        if (_target == null || bossData == null)
            return;

        float hp = bossData.currentHealth;

        if(hp < 0)
            hp = 0;

        HpBar.fillAmount = hp / Max;
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
        bossData = target.GetComponent<BossMonster>();
        if (bossData == null)
            return;
        Max = bossData.health;
    }
}
