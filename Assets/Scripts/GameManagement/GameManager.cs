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

        private void Start()
        {
            // singleton pattern
            if (gameManager == null)
            {
                gameManager = this;
            }

            GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioSource>().Stop();

            // initialize level music according to the slider
            var levelMusic = GameObject.FindGameObjectWithTag("LevelMusic");
            levelMusic.GetComponent<AudioSource>().volume = SoundMenu.Instance.GetLevelVolume();
        }


        /// <summary>
        /// Game Over Screen is shown, current wave with run until completion
        /// </summary>
        public void GameOver()
        {
            gameOverScreen.SetActive(true);
        }


        /// <returns>true if game over screen is showing</returns>
        public bool IsGameOver()
        {
            return gameOverScreen.activeSelf;
        }

        /// <summary>
        /// Method restarts the level, invoked upon start again button in game over screen inside canvas
        /// </summary>
        public void RestartLevel() //todo change so that it reload scene
        {
            SceneManager.LoadScene("SampleScene"); //Load scene called Game
        }
    }
}