using System.Collections;
using System.Collections.Generic;
using GameManagement;
using Music;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    
    /// <summary>
    /// Invoked when pause button is pressed
    /// </summary>
    public void Pause()
    {
        SoundPlayer.instance.PlayButtonClickFX();
        pauseScreen.SetActive(true);
        GameManager.isPaused = true;
        Time.timeScale = 0.0f;
    }

    

}
