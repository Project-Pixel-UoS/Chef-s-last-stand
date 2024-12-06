namespace Music
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;


    /// <summary>
    /// Responsible for managing the volume sliders
    /// </summary>
    /// <remarks>Author: Antosh</remarks>
    public class SoundMenu : MonoBehaviour
    {
        private AudioSource menuMusic;

        [SerializeField] private Slider levelVolumeSlider;
        [SerializeField] private Slider soundFXVolumeSlider;
        [SerializeField] private Slider menuVolumeSlider;


        public static readonly string LevelVolume = "levelVolume";
        public static readonly string MenuVolume = "menuVolume";
        public static readonly string SoundFXVolume = "soundFXVolume";
        

        void Awake()
        {
            menuMusic = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioSource>();
            SetVolumeSliders();
        }

        
        private void SetVolumeSliders()
        {
            levelVolumeSlider.value = PlayerPrefs.GetFloat(LevelVolume);;
            menuVolumeSlider.value = PlayerPrefs.GetFloat(MenuVolume);
            soundFXVolumeSlider.value = PlayerPrefs.GetFloat(SoundFXVolume);
        }

        public void SaveLevelVolume()
        {
            PlayerPrefs.SetFloat(LevelVolume, levelVolumeSlider.value);
        }
        
        public void SaveSoundFXVolume()
        {
            PlayerPrefs.SetFloat(SoundFXVolume, soundFXVolumeSlider.value);
        }


        public void SaveMenuVolume()
        {
            PlayerPrefs.SetFloat(MenuVolume, menuVolumeSlider.value);
            menuMusic.volume = PlayerPrefs.GetFloat(MenuVolume);
        }

   
 
    }
}