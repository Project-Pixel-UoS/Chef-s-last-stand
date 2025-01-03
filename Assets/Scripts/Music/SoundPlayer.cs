using System;
using UnityEngine;

namespace Music
{
    public class SoundPlayer:MonoBehaviour
    {

        public static SoundPlayer instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(gameObject); // game object will be permanent across different scenes
        }
        

        public void PlayPurchaseFX()
        {
            GetChildWithName("PurchaseFX").GetComponent<AudioSource>().Play();
        }
        
        private GameObject GetChildWithName(string name)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.name.Equals(name))
                {
                    return child.gameObject;
                }
            }
            return null;
        }

        public void PlayUpgradeFX()
        {
            GetChildWithName("UpgradeFX").GetComponent<AudioSource>().Play();

        }
        
        public void PlayVictoryFX()
        {
            GetChildWithName("VictoryFX").GetComponent<AudioSource>().Play();
        }
        
        public void PlayButtonClickFX()
        {
            GetChildWithName("ButtonClickFX").GetComponent<AudioSource>().Play();
        }
        
        public void PlayFailedTransactionFX()
        {
            GetChildWithName("FailedTransactionFX").GetComponent<AudioSource>().Play();
        }

        public void PlayMousePassedFX()
        {
            GetChildWithName("MousePassedFX").GetComponent<AudioSource>().Play();
        }
        
        public void PlayMoneyGainedFX()
        {
            GetChildWithName("MoneyGainedFX").GetComponent<AudioSource>().Play();
        }
    }
}