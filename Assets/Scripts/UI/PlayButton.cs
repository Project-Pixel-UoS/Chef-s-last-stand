using GameManagement;
using Level.WaveData;
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
            pauseScreen.SetActive(false);
            GameManager.isPaused = false;
            Time.timeScale = TimeScaleManager.SpeedMultiplier;
        }

    }
}