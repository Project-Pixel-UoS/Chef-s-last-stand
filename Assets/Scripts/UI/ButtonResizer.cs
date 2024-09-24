using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Attaches itself to canvas and resizes all the images that are it's children
    /// </summary>
    /// <remarks>Author: Antosh</remarks>
    public class ButtonResizer : MonoBehaviour
    {

        private void Start()
        {
            foreach (var image in gameObject.GetComponentsInChildren<Image>())
            {
                Util.Utils.ResizeSpriteInsideCanvas(image.gameObject);
            }
         
        }

    }
}