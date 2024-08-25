using System;

namespace GameManagement
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using Music;


    public class GameManager : MonoBehaviour
    {
        public static bool isPaused;
        public static float speedMultiplier;

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
 

            var menuMusic = GameObject.FindGameObjectWithTag("MenuMusic");
            if (menuMusic != null)// if statement only needed to developer mode, in case start from level scene
            {
                GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioSource>().Stop();
            }

            // initialize level music according to the slider
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