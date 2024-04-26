namespace GameManagement
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

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
        public void RestartLevel()
        {
            SceneManager.LoadScene("SampleScene"); //Load scene called Game
        }
    }
}

