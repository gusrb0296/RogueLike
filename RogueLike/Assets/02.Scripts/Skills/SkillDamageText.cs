using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillDamageText : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _destroyTime;
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private GameObject _damageTextPrefab;

    void Update()
    {
        transform.position += new Vector3(0, 0.5f, 0) * Time.deltaTime * _speed ;
    }

    public void CreateText(SkillItemData data, Transform transform)
    {
        _damageText.text = data.Atk.ToString();
        Instantiate(_damageTextPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);

    }
}
