using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [SerializeField] private float _shakeAmount = 1f;
    [SerializeField] private float _shakeTime = 0.5f;

    private void Start()
    {
        StartCoroutine(Shake(_shakeAmount, _shakeTime));
    }

    IEnumerator Shake(float shakeAmount, float shakeTime)
    {
        float timer = 0;
        while (timer <= shakeTime)
        {
            Camera.main.transform.rotation = Quaternion.Euler((Vector3)UnityEngine.Random.insideUnitCircle * shakeAmount);
            timer += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
