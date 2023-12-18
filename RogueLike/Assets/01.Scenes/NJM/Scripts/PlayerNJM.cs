using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNJM : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D Rigidbody;
    [HideInInspector ]public SkillItemData SkillData;
    private bool _isSkill;
    private bool _isCoolTime;

    [SerializeField] private Transform _skillPositionTransform;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _isSkill = false;
        _isCoolTime = false;
    }

    
    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        transform.position += new Vector3(x, 0, 0) * Time.deltaTime * 10;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody.velocity = Vector2.up * 10;
        }

        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if (_isSkill && _isCoolTime == false) Skill(SkillData);
        }
    }

    void Skill(SkillItemData data)
    {
        Instantiate(data.SkillPrefab, _skillPositionTransform.position, Quaternion.identity);
        _isCoolTime = true;
        StartCoroutine(SkillCoolTime());
    }

    IEnumerator SkillCoolTime()
    {
        yield return new WaitForSeconds(SkillData.CoolTime);
        _isCoolTime = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // SkillItem ÀÏ °æ¿ì
        if(collision.gameObject.GetComponent<SkillItem>() && collision.gameObject.GetComponent<SkillItem>().SkillData.Type == ItemType.Skiil)
        {
            _isSkill = true;
            SkillData = collision.gameObject.GetComponent<SkillItem>().SkillData;
        }
    }
}
