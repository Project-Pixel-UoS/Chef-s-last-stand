using Chef.Upgrades;
using GameManagement;
using UnityEngine;
using Util;

namespace ClickListener
{
    public class LevelClickListener : MonoBehaviour
    {
        private void Update()
        {
            ClickManager();
        }

        /// <summary> Checks what is being clicked </summary>
        /// <remarks>Maintained by: Emily Johnston </remarks>
        private void ClickManager()
        {
            if (IsMouseClicked() && Utils.CheckMousePosInsideGameStage() && !GameManager.isPaused)
            {
                GameObject clickedChef = GetClickedChef();
                ChefTracker.Instance.OnChefClicked(clickedChef);
            }
        }
        
        private bool IsMouseClicked()
        {
            return Input.GetMouseButtonDown(0);
        }

        private static RaycastHit2D CalculateMouseDownRaycast()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            int layerMask = 1 << 7; // check only for rays on layer 7 (Chef Layer)
            return Physics2D.Raycast(mousePos2D, Vector2.zero, float.PositiveInfinity, layerMask);
        }
        
        private GameObject GetClickedChef()
        {
            var clickedItem = CalculateMouseDownRaycast().collider;
            return clickedItem != null ? clickedItem.gameObject : null;
        }
    }
}