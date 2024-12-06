using System;
using UnityEngine;

using static Music.SoundMenu;

namespace Music
{
    public class PlayerPrefInitializer:MonoBehaviour
    {
        
        private static AudioSource menuMusic;
        
        private void Awake()
        {
            InitPlayerPrefs();
            menuMusic = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioSource>();
            menuMusic.volume = PlayerPrefs.GetFloat(MenuVolume);

        }
        
        private static void InitPlayerPrefs()
        {
            if (!PlayerPrefs.HasKey(LevelVolume))
            {
                PlayerPrefs.SetFloat(LevelVolume, 0.2f);
            } 
            
            if (!PlayerPrefs.HasKey(MenuVolume))
            {
                PlayerPrefs.SetFloat(MenuVolume, 0.5f);
            }


            if (!PlayerPrefs.HasKey(SoundFXVolume))
            {
                PlayerPrefs.SetFloat(SoundFXVolume, 0.2f);
            }
        }

    }
}