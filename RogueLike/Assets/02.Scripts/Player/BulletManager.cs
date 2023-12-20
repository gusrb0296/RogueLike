using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance;

    [SerializeField] private GameObject bullet;

    private bool _isCoolTime;
    private float coolTime = 0;

    private void Awake()
    {
        if(instance == null) instance = this;
    }

    private void Start()
    {
        _isCoolTime = false;
    }

    public void ShootBullet(Vector2 startPos, Vector2 direction, float range)
    {
        GameObject bulletObj = Instantiate(bullet);
        bulletObj.transform.position = startPos;
        BulletController bulletController = bulletObj.GetComponent<BulletController>();

        bulletController.Shoot(direction, range);
    }

    public void ShootSkill(SkillItemData data, Vector2 startPos, Vector2 direction)
    {
        if (_isCoolTime) return;
        GameObject skillObj = Instantiate(data.SkillPrefab, startPos, Quaternion.identity);
        skillObj.GetComponent<Skill>().SkillShoot(direction);

        _isCoolTime = true;
        StartCoroutine(SkillCoolTime(data));
    }

    IEnumerator SkillCoolTime(SkillItemData data)
    {
        coolTime = data.CoolTime;
        while (coolTime > 0.0f)
        {
            coolTime -= Time.deltaTime;

            GameManager.instance.UiManager.disable.fillAmount = coolTime / data.CoolTime;
            Debug.Log("fillAmount : " + GameManager.instance.UiManager.disable.fillAmount);

            yield return new WaitForFixedUpdate();
        }

        //yield return new WaitForSeconds(data.CoolTime);
        _isCoolTime = false;
    }
}
