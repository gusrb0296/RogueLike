using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Color _originColor = new Color(255/255f, 255/255f, 255/255f, 255/255f);
    private Color _changeColor;
    [SerializeField] private float _repeatTime;
    [SerializeField] private float _finishTime;
    private IEnumerator _coroutine;


    private void Awake()
    {
        _spriteRenderer = GameManager.instance.DataManager.Player.GetComponentInChildren<SpriteRenderer>();
    }


    public void PlayerColorChange(Color changeColor)
    {
        _coroutine = ColorInOut();
        _changeColor = changeColor;
        StartCoroutine(_coroutine);
        StartCoroutine(Stop());
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

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(_finishTime);
        StopCoroutine(_coroutine);
        _spriteRenderer.color = _originColor;
    }
}
