using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSize : MonoBehaviour
{
    public Toggle fullScreenToggle;

    public void Start()
    {
        fullScreenToggle.isOn = Screen.fullScreen;
    }
    public void ToggleFullScreen(bool isFullScreen)
    {
        if (isFullScreen)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
            fullScreenToggle.isOn=true;
        }
        else
        {
            Screen.SetResolution(1280, 960, false);
        }
    }
}
