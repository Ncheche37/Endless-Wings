using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSize : MonoBehaviour
{
   public void ToggleFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
