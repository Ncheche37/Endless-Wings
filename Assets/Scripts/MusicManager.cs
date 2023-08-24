using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance { get; private set; }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource menuMusic;

    public void PlayMenuMusic()
    {
        if(menuMusic != null)
        {
            menuMusic.Play();
        }
    }

    public void StopMenuMusic()
    {
        if(menuMusic != null)
        {
            menuMusic.Stop();
        }
    }
}
