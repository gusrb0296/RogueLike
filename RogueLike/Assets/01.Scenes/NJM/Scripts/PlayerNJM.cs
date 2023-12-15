using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNJM : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D Rigidbody;
    public SkillItemData SkillData;
    public bool IsSkill = false;

    public GameObject _skillPosition;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
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
            if(IsSkill) Skill(SkillData);
        }
    }

    void Skill(SkillItemData data)
    {
        Instantiate(data.SkillEffect, _skillPosition.transform.position, Quaternion.identity);
    }


}
