using GameManagement;
using Level.WaveData;
using Music;
using UnityEngine;

namespace UI
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] private GameObject pauseScreen;
        
        
        /// <summary>
        /// Invoked when play button is pressed
        /// </summary>
        public void Play()
        {
            SoundPlayer.instance.PlayButtonClickFX();
            pauseScreen.SetActive(false);
            GameManager.isPaused = false;
            Time.timeScale = TimeScaleManager.instance.SpeedMultiplier;
        }

    }
}