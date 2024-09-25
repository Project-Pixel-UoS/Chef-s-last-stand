using System;
using UnityEngine.UI;

namespace GameManagement
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using Music;


    public class GameManager : MonoBehaviour
    {
        public static bool isPaused;
        public static float speedMultiplier = 1;

        [SerializeField] private GameObject gameOverScreen;
        public static GameManager gameManager;
        public delegate void OnGameOver();
        public OnGameOver onGameOver;

        private void Awake()
        {
            if (gameManager == null)
            {
                gameManager = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            StopMenuMusic();
            StartLevelMusic();
            Time.timeScale = 1; 
        }

        private static void StopMenuMusic()
        {
            var menuMusic = GameObject.FindGameObjectWithTag("MenuMusic");
            if (menuMusic != null) // if statement only needed to developer mode, in case start from level scene
            {
                GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioSource>().Stop();
            }
        }

        private static void StartLevelMusic()
        {
            var levelMusic = GameObject.FindGameObjectWithTag("LevelMusic");
            levelMusic.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("levelVolume");
        }

        public void GameOver()
        {
            gameOverScreen.SetActive(true);
            onGameOver?.Invoke();
        }


        /// <returns>true if game over screen is showing</returns>
        public bool IsGameOver()
        {
            return gameOverScreen.activeSelf;
        }

        /// <summary>
        /// Invoked upon start again button in game over screen inside canvas
        /// </summary>
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}