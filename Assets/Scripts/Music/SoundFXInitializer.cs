
namespace Music
{
    using UnityEngine;
    
    /// <summary>
    /// Responsible for adjusting volume of game objects sound effects
    /// Should be attached to any game object that makes sound
    /// </summary>
    /// <remarks>Author: Antosh</remarks>
    public class SoundFXInitializer : MonoBehaviour
    {
        void Start()
        {
            foreach (var audioSource in GetComponentsInChildren<AudioSource>())
            {
                audioSource.volume = PlayerPrefs.GetFloat(SoundMenu.SoundFXVolume);
            }
        }
    }
}