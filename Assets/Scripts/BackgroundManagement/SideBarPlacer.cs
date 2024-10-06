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
                AddTilesToTheLeftOfGameStage(3);
                AddTilesToTheRightOfGameStage(3);
            }
            else
            {
                AddTilesAboveGameStage(3);
                AddTilesBelowGameStage(3);

                
            }
        }
        
        private void AddTilesAboveGameStage(int numOfBricksToPlace)
        {
            for (int counter = 0; counter <= numOfBricksToPlace; counter++)
            {
                GameObject tile = Instantiate(bricksPrefab);
                Vector2 pos = GetTopRightGameStagePos();
                var brickDimensions = GetTileDimensions(tile);
                pos.x -= brickDimensions.x * (0.5f + counter); 
                pos.y += brickDimensions.y / 2f;
                tile.transform.position = pos;
            }
        }
        
        private void AddTilesBelowGameStage(int numOfBricksToPlace)
        {
            for (int counter = 0; counter <= numOfBricksToPlace; counter++)
            {
                GameObject tile = Instantiate(bricksPrefab);
                Vector2 pos = GetBottomLeftGameStagePos();
                var brickDimensions = GetTileDimensions(tile);
                pos.x += brickDimensions.x * (0.5f + counter); 
                pos.y -= brickDimensions.y / 2f;
                tile.transform.position = pos;
            }
        }



        private void AddTilesToTheRightOfGameStage(int numOfBricksToPlace)
        {
            for (int counter = 0; counter <= numOfBricksToPlace; counter++)
            {
                GameObject tile = Instantiate(bricksPrefab);
                Vector2 pos = GetTopRightGameStagePos();
                var brickDimensions = GetTileDimensions(tile);
                pos.x += brickDimensions.x / 2f;
                pos.y -= brickDimensions.y * (0.5f + counter);
                tile.transform.position = pos;
            }
        }

  

        private void AddTilesToTheLeftOfGameStage(int numOfBricksToPlace)
        {
            for (int counter = 0; counter <= numOfBricksToPlace; counter++)
            {
                GameObject tile = Instantiate(bricksPrefab);
                Vector2 pos = GetBottomLeftGameStagePos();
                var brickDimensions = GetTileDimensions(tile);
                pos.x -= brickDimensions.x / 2f; 
                pos.y += brickDimensions.y * (0.5f + counter);
                tile.transform.position = pos;
            }
        }

        Vector2 GetBottomLeftGameStagePos()
        {
            return GetBottomBarCorner(0);
        }
        
        Vector2 GetTopRightGameStagePos()
        {
            return GetSideBarCorner(2);
        }

        private Vector2 GetBottomBarCorner(int cornerIndex)
        {
            var bottomBar = GameObject.FindGameObjectWithTag("BottomBar");
            return GetItemCorner(cornerIndex, bottomBar);
        }
        
        private Vector2 GetSideBarCorner(int cornerIndex)
        {
            var sideBar = GameObject.FindGameObjectWithTag("SideBar");
            return GetItemCorner(cornerIndex, sideBar);
        }

        private static Vector2 GetItemCorner(int cornerIndex, GameObject bottomBar)
        {
            RectTransform rectTransform = bottomBar.GetComponent<RectTransform>();
            Vector3[] worldCorners = new Vector3[4];
            rectTransform.GetWorldCorners(worldCorners);
            return worldCorners[cornerIndex];
        }
        private static Vector3 GetTileDimensions(GameObject tile)
        {
            return tile.GetComponent<SpriteRenderer>().sprite.bounds.size;
        }
    }
}