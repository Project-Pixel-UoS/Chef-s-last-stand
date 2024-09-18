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

        [SerializeField] private GameObject bricksPrefab;

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
            if (menuMusic != null) // if statement only needed to developer mode, in case start from level scene
            {
                GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioSource>().Stop();
            }

            // initialize level music according to the slider
            var levelMusic = GameObject.FindGameObjectWithTag("LevelMusic");
            levelMusic.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("levelVolume");
            AddBackground();
        }

        private void AddBackground()
        {
            if (IsScreenToWide())
            {
                int numOfBricksToPlace = 3;
                AddBrickToTheLeftOfGameStage(numOfBricksToPlace);
                AddBrickToTheRightOfGameStage(numOfBricksToPlace);
            }
            else
            {
                // Code for placing bricks on top and bottom needs to be here - not enough time.
            }
        }

        private void AddBrickToTheRightOfGameStage(int numOfBricksToPlace)
        {
            for (int counter = 1; counter <= numOfBricksToPlace; counter++)
            {
                GameObject bricks = Instantiate(bricksPrefab);

                float rightBorderXScreenUnits = GetRightBorderXInScreenUnits();
                float bottomBorderYScreenUnits = 0;

                var positionScreenUnits = new Vector2(rightBorderXScreenUnits, bottomBorderYScreenUnits);
                Vector2 pos = Camera.main.ScreenToWorldPoint(positionScreenUnits);

                var brickDimensions = bricks.GetComponent<SpriteRenderer>().sprite.bounds.size;
                pos.x += brickDimensions.x / 2f;
                pos.y += brickDimensions.y * counter / 2f;

                bricks.transform.position = pos;
            }
        }

        private void AddBrickToTheLeftOfGameStage(int numOfBricksToPlace)
        {
            // harde coded number of times to place the bricks - not ideal but ran out of time
            for (int counter = 1; counter <= numOfBricksToPlace; counter++)
            {
                GameObject bricks = Instantiate(bricksPrefab);

                float leftBorderXScreenUnits = GetLeftBorderXInScreenUnits();
                float bottomBorderYScreenUnits = 0;

                var positionScreenUnits = new Vector2(leftBorderXScreenUnits, bottomBorderYScreenUnits);
                Vector2 pos = Camera.main.ScreenToWorldPoint(positionScreenUnits);

                var brickDimensions = bricks.GetComponent<SpriteRenderer>().sprite.bounds.size;
                pos.x -= brickDimensions.x / 2f;
                pos.y += brickDimensions.y * counter / 2f;

                bricks.transform.position = pos;
            }
        }

        private static bool IsScreenToWide()
        {
            float currentAspectRatio = (float)Screen.width / Screen.height;
            float targetAspectRatio = (float)16 / 9;
            return currentAspectRatio >= targetAspectRatio;
        }


        /// <returns>left most x co ord of game stage </returns>
        private static float GetLeftBorderXInScreenUnits()
        {
            var widthOfGameStage = CalculateWidthOfGameStage();
            return (Screen.width - widthOfGameStage) / 2f;
        }

        /// <returns>left most x co ord of game stage </returns>
        private static float GetRightBorderXInScreenUnits()
        {

            var widthOfGameStage = CalculateWidthOfGameStage();
            return (Screen.width + widthOfGameStage) / 2f;
        }

        /// <summary>
        /// The bottom bar's width property is always fixed at 1920 regardless the screen width, so if we play at
        /// a screen width smaller than 1920, we dont want to deduct the full bottom bar's width.
        /// </summary>
        private static float CalculateWidthOfGameStage()
        {
            var bottomBar = GameObject.FindGameObjectWithTag("BottomBar");
            Rect rect = bottomBar.GetComponent<RectTransform>().rect;
            return Math.Min(rect.width, Screen.width);
        }


        /// <returns>left most x co ord of game stage </returns>
        private static float GetBottomBorderYInScreenUnits()
        {
            var sideBar = GameObject.FindGameObjectWithTag("SideBar");
            var bottomBar = GameObject.FindGameObjectWithTag("BottomBar");
            Rect rect = sideBar.GetComponent<RectTransform>().rect;
            float bottomBarHeight = bottomBar.GetComponent<RectTransform>().rect.height;
            float gameStageHeight = Math.Min(rect.height, Screen.height) + bottomBarHeight;
            return (Screen.height - gameStageHeight) / 2f;
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