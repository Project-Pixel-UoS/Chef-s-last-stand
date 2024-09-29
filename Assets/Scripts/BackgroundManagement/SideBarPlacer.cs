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
                bricks.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;

                float leftBorderXScreenUnits = GetLeftBorderXInScreenUnits();
                var positionScreenUnits = new Vector2(leftBorderXScreenUnits, 0);
                Vector2 pos = Camera.main.ScreenToWorldPoint(positionScreenUnits);

                var brickDimensions = bricks.GetComponent<SpriteRenderer>().sprite.bounds.size;
                pos.x -= brickDimensions.x / 2f;
                pos.y += brickDimensions.y * counter / 2f;

                bricks.transform.position = pos;
            }
        }

        float GetLeftBorderXInScreenUnits()
        {
            return GetBottomBarCorner(0).x;
        }
        
        float GetRightBorderXInScreenUnits()
        {
            return GetBottomBarCorner(3).x;
        }

        private Vector2 GetBottomBarCorner(int cornerIndex)
        {
            var bottomBar = GameObject.FindGameObjectWithTag("BottomBar");
            RectTransform rectTransform = bottomBar.GetComponent<RectTransform>();
            Vector3[] worldCorners = new Vector3[4];
            rectTransform.GetWorldCorners(worldCorners);
            Vector3 corner = worldCorners[cornerIndex];
            return Camera.main.WorldToScreenPoint(corner);
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