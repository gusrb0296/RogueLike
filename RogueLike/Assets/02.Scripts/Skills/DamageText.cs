using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private GameObject _damageTextPrefab;
    [SerializeField] private Vector3 _position;

    void Update()
    {
        transform.position += new Vector3(0, 1, 0) * Time.deltaTime * _speed ;
    }

    public void CreateSkillText(SkillItemData data, Transform transform, float velocityX)
    {
        _damageText.text = data.Atk.ToString();
        _damageText.color = new Color(255/255f, 47/255f, 27/255f, 255/255f);

        int dir = (velocityX > 0) ? 1 : -1;

        Instantiate(_damageTextPrefab, transform.position + new Vector3(dir * _position.x, _position.y, _position.z), Quaternion.identity);
    }

    public void CreateNormalAttackText(Transform transform, float velocityX)
    {
        _damageText.text = GameManager.instance.DataManager.PlayerCurrentStats.attackSO.power.ToString();
        _damageText.color = new Color(91/255f, 89/255f, 89/255f, 255/255f);

        int dir = (velocityX > 0) ? 1 : -1;

        Instantiate(_damageTextPrefab, transform.position + new Vector3(dir * _position.x, _position.y, _position.z), Quaternion.identity);
    }
}
