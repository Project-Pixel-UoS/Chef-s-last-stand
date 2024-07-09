using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    
    private AudioSource audioSource;
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject); // game object will be permanent across different scenes
    }

    public void Play()
    {
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}
