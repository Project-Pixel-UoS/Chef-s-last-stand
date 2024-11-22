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

        public static SoundMenu Instance { get; private set; }


        void Awake()
        {
            SetupSingletonField();
            menuMusic = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioSource>();
            SetVolumeSliders();
        }

        private void SetupSingletonField()
        {
            // // If there is an instance, and it's not me, delete myself.
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private List<AudioSource> GetSoundEffects()
        {
            List<AudioSource> audioSources = new List<AudioSource>();
            foreach (var soundEffect in GameObject.FindGameObjectsWithTag("SoundFX"))
            {
                audioSources.Add(soundEffect.GetComponent<AudioSource>());
            }

            return audioSources;
        }

        private void SetVolumeSliders()
        {
            if (!PlayerPrefs.HasKey("backgroundMusicVolume")){
                PlayerPrefs.SetFloat("backgroundMusicVolume", 1);

            if (!PlayerPrefs.HasKey("soundEffectVolume"))
                PlayerPrefs.SetFloat("soundEffectVolume", 1);

            if (!PlayerPrefs.HasKey("menuVolume"))
                PlayerPrefs.SetFloat("menuVolume", 1);


            LoadLevelVolume();
            LoadMenuVolume();
            LoadSoundFXVolume();
        }


        // public void ChangeSoundFXVolume()
        // {
        //     SaveSoundFXVolume();
        // }
        //
        // public void ChangeLevelMusicVolume()
        // {
        //     SaveLevelVolume();
        // }

        public void ChangeMenuMusicVolume()
        {
            SaveMenuVolume();
            SetMenuMusicVolume();
        }


        //Loading and saving functions
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

        public float GetLevelVolume()
        {
            return PlayerPrefs.GetFloat("levelVolume");
        }

        public float GetMenuVolume()
        {
            return PlayerPrefs.GetFloat("menuVolume");
        }

        public float GetSoundFXVolume()
        {
            return PlayerPrefs.GetFloat("soundFXVolume");
        }

        public void SetMenuMusicVolume()
        {
            menuMusic.volume = GetMenuVolume();
        }
    }
}