using System;
using UnityEngine;

namespace BackgroundManagement
{
    public class SideBarPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject bricksPrefab;

        private void Start()
        {
            if (Util.Utils.IsScreenToWide())
            {
                int numOfBricksToPlace = 3;
                AddBrickToTheLeftOfGameStage(numOfBricksToPlace);
                AddBrickToTheRightOfGameStage(numOfBricksToPlace);
            }
        }

 

        private void AddBrickToTheRightOfGameStage(int numOfBricksToPlace)
        {
            for (int counter = 1; counter <= numOfBricksToPlace; counter++)
            {
                GameObject bricks = Instantiate(bricksPrefab);

                float rightBorderXScreenUnits = GetRightBorderXInScreenUnits();
                var positionScreenUnits = new Vector2(rightBorderXScreenUnits, 0);
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
                var positionScreenUnits = new Vector2(leftBorderXScreenUnits, 0);
                Vector2 pos = Camera.main.ScreenToWorldPoint(positionScreenUnits);

                var brickDimensions = bricks.GetComponent<SpriteRenderer>().sprite.bounds.size;
                pos.x -= brickDimensions.x / 2f;
                pos.y += brickDimensions.y * counter / 2f;

                bricks.transform.position = pos;
            }
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
    }
}