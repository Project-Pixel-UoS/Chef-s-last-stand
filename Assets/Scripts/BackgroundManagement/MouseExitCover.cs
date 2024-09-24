using GameManagement;
using UnityEngine;

namespace BackgroundManagement
{
    public class MouseExitCover : MonoBehaviour
    {
        private void Start()
        {
            // GameManager.gameManager.onGameOver += Hide;
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}