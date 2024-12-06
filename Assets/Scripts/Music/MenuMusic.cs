namespace Music
{

    using UnityEngine;

    /// <summary>
    /// To be attached to 'Menu Music' game object.
    /// Responsible for music continuing as you switch scene
    /// </summary>
    /// <remarks>Author: Antosh</remarks>
    public class MenuMusic : MonoBehaviour
    {
        private AudioSource audioSource;


        void Awake()
        {
            HandleMultipleMenuMusics();
            DontDestroyOnLoad(gameObject); // game object will be permanent across different scenes


        }


        private void HandleMultipleMenuMusics()
        {
            // this can happen if you go back to the title screen where the menu music is created
            if (CheckMenuMusicAlreadyPlaying())
            {
                Destroy(gameObject);
            }
        }


        private static bool CheckMenuMusicAlreadyPlaying()
        {
            return GameObject.FindGameObjectsWithTag("MenuMusic").Length > 1;
        }
    }
}