using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Util
{
    public class Utils : MonoBehaviour
    {
        /// <returns> False if mouse position is outside of the action zone, where the game is happening.
        /// This includes the bottom and side bar</returns>
        /// <remarks>Author: Antosh</remarks>
        public static bool CheckMousePosInsideGameStage()
        {
            var gameStage = GameObject.FindGameObjectWithTag("GameStage");
            Vector3 [] corners = GetItemCorners(gameStage);
            Vector2 bottomLeft = corners[0];
            Vector2 topRight = corners[2];
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return mouseWorldPos.x > bottomLeft.x && mouseWorldPos.y > bottomLeft.y && 
                   mouseWorldPos.x < topRight.x && mouseWorldPos.y < topRight.y;
        }
        
        public static void PlayInvalidTransactionSound(GameObject gameObject)
        {
            GetChildWithTag(gameObject, "FailedTransactionFX").GetComponent<AudioSource>().Play();
        }
        
        public static void PlayMoneyGainedFX(GameObject gameObject)
        {
            GetChildWithTag(gameObject, "MoneyGainedFX").GetComponent<AudioSource>().Play();
        }
        
        public static void PlayShootSound(GameObject chef)
        {
            GetChildWithTag(chef, "ProjectileThrowFX").GetComponent<AudioSource>().Play();
        }

        public static void StopShootSound(GameObject chef)
        {
            GetChildWithTag(chef, "ProjectileThrowFX").GetComponent<AudioSource>().Stop();
        }
        
        public static void PlayMousePassesFX(GameObject gameObject)
        {
            print("PLAYING MOUSE PASSED FX");
            GetChildWithTag(gameObject, "MousePassedFX").GetComponent<AudioSource>().Play();

        }

        public static GameObject GetChildWithTag(GameObject parent, string tag)
        {
            foreach (Transform child in parent.transform)
            {
                if (child.gameObject.CompareTag(tag))
                {
                    return child.gameObject;
                }
            }

            return null;
        }

        public static bool IsScreenToWide()
        {
            float currentAspectRatio = (float)Screen.width / Screen.height;
            float targetAspectRatio = (float)16 / 9;
            return currentAspectRatio >= targetAspectRatio;
        }


        public static void ResizeSpriteOutsideCanvas(GameObject sprite)
        {
            sprite.transform.localScale *= GetOutsideCanvasResizeMultiplier();
        }

        public static float ResizeRadiusOutsideCanvas(float radius)
        {
            return radius * GetOutsideCanvasResizeMultiplier();
        }

        private static float GetOutsideCanvasResizeMultiplier()
        {
            float aspectRatio = ((float)Screen.width / Screen.height);
            float targetAspectRatio = 1920 / 1080f;
            float multiplier = 1f;
            if (aspectRatio <= targetAspectRatio)
                multiplier *= aspectRatio / targetAspectRatio;
            return multiplier;
        }

        public static void ResizeSpriteInsideCanvas(GameObject sprite)
        {
            sprite.transform.localScale *= 1080f / Screen.height;
        }
        
        public static Vector2 GetItemCorner(GameObject gameObject, int cornerIndex)
        {
            return GetItemCorners(gameObject)[cornerIndex];
        }
        
        public static Vector3[] GetItemCorners(GameObject bottomBar)
        {
            RectTransform rectTransform = bottomBar.GetComponent<RectTransform>();
            Vector3[] worldCorners = new Vector3[4];
            rectTransform.GetWorldCorners(worldCorners);
            return worldCorners;
        }



    }
}