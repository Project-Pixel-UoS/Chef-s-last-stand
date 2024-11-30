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



        void Awake()
        {
            menuMusic = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioSource>();
            SetVolumeSliders();
        }

        
        private void SetVolumeSliders()
        {
            if (!PlayerPrefs.HasKey("backgroundMusicVolume"))
                PlayerPrefs.SetFloat("backgroundMusicVolume", 0.5f);

            if (!PlayerPrefs.HasKey("soundEffectVolume"))
                PlayerPrefs.SetFloat("soundEffectVolume", 0.5f);

            if (!PlayerPrefs.HasKey("menuVolume"))
                PlayerPrefs.SetFloat("menuVolume", 0.5f);


            LoadLevelVolume();
            LoadMenuVolume();
            LoadSoundFXVolume();
        }

        

        public void ChangeMenuMusicVolume()
        {
            SaveMenuVolume();
            SetMenuMusicVolume();
        }


        private void LoadLevelVolume()
        {
            levelVolumeSlider.value = GetLevelVolume();
        }

        public void SaveLevelVolume()
        {
            PlayerPrefs.SetFloat("levelVolume", levelVolumeSlider.value);
        }

        private void LoadSoundFXVolume()
        {
            soundFXVolumeSlider.value = GetSoundFXVolume();
        }

        public void SaveSoundFXVolume()
        {
            PlayerPrefs.SetFloat("soundFXVolume", soundFXVolumeSlider.value);
        }

        private void LoadMenuVolume()
        {
            menuVolumeSlider.value = GetMenuVolume();
        }

        private void SaveMenuVolume()
        {
            PlayerPrefs.SetFloat("menuVolume", menuVolumeSlider.value);
        }

        private float GetLevelVolume()
        {
            return PlayerPrefs.GetFloat("levelVolume");
        }

        private float GetMenuVolume()
        {
            return PlayerPrefs.GetFloat("menuVolume");
        }

        private float GetSoundFXVolume()
        {
            return PlayerPrefs.GetFloat("soundFXVolume");
        }

        private void SetMenuMusicVolume()
        {
            menuMusic.volume = GetMenuVolume();
        }
    }
}