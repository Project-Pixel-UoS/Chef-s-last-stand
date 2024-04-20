using Unity.VisualScripting;
using UnityEngine;

namespace Chef.Upgrades
{
    
    /// <summary>
    /// Keeps track of the currently selected chef by the player
    /// </summary>
    /// <remarks>Author: Antosh</remarks>
    public class ChefTracker : MonoBehaviour
    {
        
        public static ChefTracker Instance { get; private set; } // singleton pattern

        [SerializeField] private GameObject upgradeRangeUI; // game object containing upgrade button and upgrade bar
        [SerializeField] private GameObject upgradeSpecialUI;
        [SerializeField] private GameObject[] PrepCookUpgrades;
        [SerializeField] private GameObject[] PrepCookProjUpgrades;


        private GameObject currentChef;
        public GameObject CurrentChef // property of current selected chef (chef that's been most recently clicked)
        {
            set => SelectNewChef(value);
        } 

        private void Awake()
        {
            print("CHEF TRACKER AWAKE============================");
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            } 
        }

        /// <summary>
        /// Method invoked each the currentChef field is updated
        /// </summary>
        /// <param name="chef">The new current chef being set, could be null</param>
        private void SelectNewChef(GameObject chef)
        {
            currentChef = chef;
            
            if (chef == null)
            {
                // remove upgrade buttons
                upgradeRangeUI.SetActive(false);
                upgradeSpecialUI.SetActive(false);
            }
            else
            {
                //display upgrade buttons corresponding to current chef
                upgradeRangeUI.SetActive(true);
                currentChef.GetComponent<UpgradeTracker>().RefreshRangeBar();
                upgradeSpecialUI.SetActive(true);
                currentChef.GetComponent<UpgradeTracker>().RefreshSpecialBar();
            }
            
        }

        /// <summary>
        /// Invoked when 'Upgrade Range' button on the bottom bar is clicked
        /// </summary>
        public void UpgradeCurrentChefPath1()
        {
            currentChef.GetComponent<UpgradeTracker>().UpgradePath1();
        }
        
        /// <summary>
        /// Invoked when 'Upgrade Range' button on the bottom bar is clicked
        /// </summary>
        public void UpgradeCurrentChefPath2()
        {
            currentChef.GetComponent<UpgradeTracker>().UpgradePath2();
        }

        public GameObject[] GetPrepCooks()
        {
            return PrepCookUpgrades;
        }

    }
}