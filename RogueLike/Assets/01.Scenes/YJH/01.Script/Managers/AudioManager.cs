using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // 필요한 효과음이 있다면 주석으로 적어놔 주세요. 보고 추가해두겠습니다.

    private AudioSource _backgroundMusic;
    private AudioSource _soundEffects;

    public void Initalize()
    {
        if (_backgroundMusic == null && _soundEffects == null)
        {
            _backgroundMusic = GetComponent<AudioSource>();
            _soundEffects = GetComponent<AudioSource>();
        }
    }
}
