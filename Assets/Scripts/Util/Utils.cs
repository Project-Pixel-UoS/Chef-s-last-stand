using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Util
{
    public class Utils:MonoBehaviour
    {
  
        /// <returns> False if mouse position is outside of the action zone, where the game is happening.
        /// This includes the bottom and side bar</returns>
        /// <remarks>Author: Antosh</remarks>
        public static bool checkMousePosOutsideMap()
        {
            Image sideBar = GameObject.FindWithTag("SideBar").GetComponent<Image>();
            Image bottomBar = GameObject.FindWithTag("BottomBar").GetComponent<Image>();

            
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float orthographicSize = Camera.main.orthographicSize;
            // Calculate the width using the aspect ratio of the screen
            float aspectRatio = Screen.width / (float)Screen.height;
        
            float cameraUnitWidth = orthographicSize * 2 * aspectRatio;
            float cameraUnitHeight = orthographicSize * 2f;

            float sideBarWidth = (cameraUnitWidth * sideBar.rectTransform.rect.width) / Screen.width;
            float sideBarBound = sideBar.transform.position.x - sideBarWidth / 2;
        
            float bottomBarHeight = (cameraUnitHeight * bottomBar.rectTransform.rect.height) / Screen.height;
            float bottomBarBound = bottomBar.transform.position.y + bottomBarHeight / 2;

            if (worldPos.x <= -8 || worldPos.x >= sideBarBound)
            {
                return true;
            }

            if (worldPos.y <= bottomBarBound || worldPos.y >= 4.5)
            {
                return true;
            }

            return false;

        }

        public static void PlayShootSound(GameObject chef)
        {
            GetChildWithTag(chef, "ProjectileThrowFX").GetComponent<AudioSource>().Play();
        }
        public static void StopShootSound(GameObject chef)
        {
            GetChildWithTag(chef, "ProjectileThrowFX").GetComponent<AudioSource>().Stop();

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
        


     
    }
}