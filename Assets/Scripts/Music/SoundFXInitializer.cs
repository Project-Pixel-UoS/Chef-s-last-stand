using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXInitializer : MonoBehaviour
{
    void Start()
    {
        foreach (var audioSource in GetComponentsInChildren<AudioSource>())
        {
            audioSource.volume = SoundMenu.Instance.GetSoundEffectsVolume();
        }
    }
}
