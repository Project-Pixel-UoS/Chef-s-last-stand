using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject backgroundSpawner;
    // private BackgroundMovement backgroundMovement;

    private List<AudioSource> soundEffectAudioSources;

    [SerializeField] private Slider backgroundVolumeSlider;
    [SerializeField] private Slider soundEffectVolumeSlider;
    [SerializeField] private Slider menuVolumeSlider;

    // public float menuVolume;


    public static SoundMenu Instance { get; private set; }


    void Awake()
    {
        SetupSingletonField();
        soundEffectAudioSources = GetSoundEffects();
        // backgroundMovement = backgroundSpawner.GetComponent<BackgroundMovement>();
        SetVolumeSliders();
        gameObject.SetActive(false); //game object must start of active in order for player prefs to function
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
        foreach (var soundEffect in GameObject.FindGameObjectsWithTag("SoundEffect"))
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


        LoadBackgroundMusic();
        LoadMenuVolume();
        LoadSoundEffectVolume();
    }

    public void ToggleSoundsMenu()
    {
        pauseMenuUI.SetActive(false);
        gameObject.SetActive(true); //enable the sound menu
    }

    public void Back()
    {
        pauseMenuUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ChangeBackgroundMusicVolume()
    {
        // AudioListener.volume = backgroundVolumeSlider.value;
        SaveBackgroundMusic();
        // backgroundMovement.audioSourceOne.volume = backgroundVolumeSlider.value;
        // backgroundMovement.audioSourceTwo.volume = backgroundVolumeSlider.value;
    }

    public void ChangeSoundsEffectVolume()
    {
        SaveSoundEffectVolume();
        foreach (var soundEffect in soundEffectAudioSources)
        {
            if (soundEffect != null)
            {
                soundEffect.volume = soundEffectVolumeSlider.value;
            }
        }
    }

    // public void ChangeMenuMusicVolume()
    // {
    //     SaveMenuVolume();
    //     // menuVolume = menuVolumeSlider.value;
    // }
    //


    //Loading and saving functions
    private void LoadBackgroundMusic()
    {
        backgroundVolumeSlider.value = PlayerPrefs.GetFloat("backgroundMusicVolume");
    }

    private void SaveBackgroundMusic()
    {
        PlayerPrefs.SetFloat("backgroundMusicVolume", backgroundVolumeSlider.value);
    }

    private void LoadSoundEffectVolume()
    {
        soundEffectVolumeSlider.value = PlayerPrefs.GetFloat("soundEffectVolume");
    }

    private void SaveSoundEffectVolume()
    {
        PlayerPrefs.SetFloat("soundEffectVolume", soundEffectVolumeSlider.value);
    }

    private void LoadMenuVolume()
    {
        menuVolumeSlider.value = PlayerPrefs.GetFloat("menuVolume");
    }

    private void SaveMenuVolume()
    {
        PlayerPrefs.SetFloat("menuVolume", menuVolumeSlider.value);
    }

    public float GetBackgroundVolume()
    {
        return backgroundVolumeSlider.value;
    }

    // public float GetMenuVolume()
    // {
    //     return menuVolumeSlider.value;
    // } 

    public float GetSoundEffectsVolume()
    {
        return soundEffectVolumeSlider.value;
    }
}