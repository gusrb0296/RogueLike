using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Color _originColor;
    private Color _changeColor;
    [SerializeField] private float _repeatTime;
    [SerializeField] private float _finishTime;
    private IEnumerator _coroutine;
    private float time = 0;


    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        time += Time.deltaTime; 
        if(time > _finishTime)
        {
            StopAllCoroutines();
            _spriteRenderer.color = _originColor;
        }
    }


    public void PlayerColorChange(Color changeColor)
    {
        time = 0;
        _coroutine = ColorInOut();
        _originColor = _spriteRenderer.color;
        _changeColor = changeColor;
        StartCoroutine(_coroutine);
    }


    private IEnumerator ColorInOut()
    {
        while (true)
        {
            yield return StartCoroutine(ColorChanges(_originColor, _changeColor));
            yield return StartCoroutine(ColorChanges(_changeColor, _originColor));
        }
    }

    private IEnumerator ColorChanges(Color start, Color end)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / _repeatTime;

            _spriteRenderer.color = Color.Lerp(start, end, _repeatTime);

            yield return null;
        }
    }
}
