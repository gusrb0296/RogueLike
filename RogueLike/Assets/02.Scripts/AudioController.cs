using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    private Slider volume;
    private bool isMute = false;
    private float muteVolume;
    private void Awake()
    {
        volume = GetComponent<Slider>();
    }

    private void Start()
    {
        volume.value = GameManager.instance.AudioManager.GetVolume();
    }

    private void Update()
    {
        if (isMute && volume.value > 0) isMute = false;
        GameManager.instance.AudioManager.SetVolume(volume.value);
    }

    public void VolumeDown()
    {
        Debug.Log("Sound Down");
        if (isMute)
        {
            volume.value = muteVolume;
        }
        else
        {
            muteVolume = volume.value;
            volume.value = 0;
        }
        isMute = !isMute;
    }
}
