using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{

    public Slider volumeSlider;
    void Start()
    {
        volumeSlider.value = AudioListener.volume;
    }

    public void OnVolumeSliderChanged(float volume)
    {
        AudioListener.volume = volume;
    }

   
}
