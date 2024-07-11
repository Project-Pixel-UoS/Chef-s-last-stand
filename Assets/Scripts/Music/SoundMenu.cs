using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundMenu : MonoBehaviour
{
    // [SerializeField] private GameObject pauseMenuUI;
    // [SerializeField] private GameObject backgroundSpawner;

    private AudioSource menuMusic;
    // private BackgroundMovement backgroundMovement;

    // private List<AudioSource> soundEffectAudioSources;

    [SerializeField] private Slider levelVolumeSlider;
    [SerializeField] private Slider soundFXVolumeSlider;
    [SerializeField] private Slider menuVolumeSlider;

    // public float menuVolume;


    public static SoundMenu Instance { get; private set; }


    void Awake()
    {
        SetupSingletonField();
        menuMusic = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioSource>();
        SetVolumeSliders();
        // soundEffectAudioSources = GetSoundEffects();
        // gameObject.SetActive(false); //game object must start of active in order for player prefs to function
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
        if (!PlayerPrefs.HasKey("backgroundMusicVolume"))
            PlayerPrefs.SetFloat("backgroundMusicVolume", 1);

        if (!PlayerPrefs.HasKey("soundEffectVolume"))
            PlayerPrefs.SetFloat("soundEffectVolume", 1);

        if (!PlayerPrefs.HasKey("menuVolume"))
            PlayerPrefs.SetFloat("menuVolume", 1);


        LoadLevelVolume();
        LoadMenuVolume();
        LoadSoundFXVolume();
    }

    // public void ToggleSoundsMenu()
    // {
    //     pauseMenuUI.SetActive(false);
    //     gameObject.SetActive(true); //enable the sound menu
    // }

    // public void Back()
    // {
    //     pauseMenuUI.SetActive(true);
    //     gameObject.SetActive(false);
    // }

    // public void ChangeLevelMusicVolume()
    // {
    //     // AudioListener.volume = backgroundVolumeSlider.value;
    //     SaveBackgroundMusic();
    //     // backgroundMovement.audioSourceOne.volume = backgroundVolumeSlider.value;
    //     // backgroundMovement.audioSourceTwo.volume = backgroundVolumeSlider.value;
    // }

    public void ChangeSoundFXVolume()
    {
        SaveSoundFXVolume();
        // foreach (var soundEffect in soundEffectAudioSources)
        // {
        //     if (soundEffect != null)
        //     {
        //         soundEffect.volume = soundEffectVolumeSlider.value;
        //     }
        // }
    }
    
    public void ChangeLevelMusicVolume()
    {
        SaveLevelVolume();
        // foreach (var soundEffect in soundEffectAudioSources)
        // {
        //     if (soundEffect != null)
        //     {
        //         soundEffect.volume = soundEffectVolumeSlider.value;
        //     }
        // }
    }

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

    private void SaveLevelVolume()
    {
        PlayerPrefs.SetFloat("levelVolume", levelVolumeSlider.value);
    }

    private void LoadSoundFXVolume()
    {
        soundFXVolumeSlider.value = GetSoundFXVolume();
    }

    private void SaveSoundFXVolume()
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