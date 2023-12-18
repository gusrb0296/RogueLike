using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveCahr : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
