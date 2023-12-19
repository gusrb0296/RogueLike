using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance;

    [SerializeField] private GameObject bullet;

    private bool _isCoolTime;

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
        yield return new WaitForSeconds(data.CoolTime);
        _isCoolTime = false;
    }
}
