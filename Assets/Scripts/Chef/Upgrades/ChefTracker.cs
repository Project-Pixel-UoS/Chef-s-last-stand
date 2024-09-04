using Shop;
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

        [SerializeField] private float sellChefFraction;
        [SerializeField] private GameObject upgradeRangeUI; // game object containing upgrade button and upgrade bar
        [SerializeField] private GameObject upgradeSpecialUI;
        [SerializeField] private TMPro.TextMeshProUGUI rangeCostTextObject;
        [SerializeField] private TMPro.TextMeshProUGUI abilityCostTextObject;
        [SerializeField] private GameObject sellChefUI;
        [SerializeField] private GameObject[] prepCookUpgrades;
        [SerializeField] private GameObject[] grillardinUpgrades;
        [SerializeField] private GameObject[] waiterUpgrades;
        [SerializeField] private GameObject[] entremetierUpgrades;
        private ShopSlotManager upgradeManager;

        private GameObject currentChef;
        public GameObject CurrentChef // property of current selected chef (chef that's been most recently clicked)
        {
            get => currentChef;
            set => SelectNewChef(value);
        } 

        private void Awake()
        {
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
                sellChefUI.SetActive(false);
            }
            else
            {
                upgradeManager = chef.GetComponent<ShopSlotManager>();

                //display upgrade buttons corresponding to current chef

                upgradeRangeUI.SetActive(true);
                currentChef.GetComponent<UpgradeTracker>().RefreshRangeBar();
                rangeCostTextObject.text = ("Upgrade range\n(" + upgradeManager.getRangeCost() + ")");

                upgradeSpecialUI.SetActive(true);
                currentChef.GetComponent<UpgradeTracker>().RefreshSpecialBar();
                abilityCostTextObject.text = ("Upgrade ability\n(" + upgradeManager.getChefCost() + ")");

                sellChefUI.SetActive(true);

                TMPro.TextMeshProUGUI sellText = sellChefUI.GetComponentInChildren<TMPro.TextMeshProUGUI>();
                sellText.text = "Sell chef\n" + "("+getSellCost()+")";
            }
            
        }

        /// <summary>
        /// Invoked when 'Upgrade Range' button on the bottom bar is clicked
        /// </summary>
        public void UpgradeCurrentChefPath1()
        {
            var chef = currentChef.GetComponent<UpgradeTracker>();
            if (chef.getPath1Status() != 4 && upgradeManager.checkSufficientFunds(upgradeManager.getChefCost()))
            {
                CreditManager creditManager = GameObject.FindGameObjectWithTag("Credits").GetComponent<CreditManager>();
                creditManager.SpendCredits(upgradeManager.getRangeCost());
                currentChef.GetComponent<UpgradeTracker>().UpgradePath1();
            }
            
        }
        
        /// <summary>
        /// Invoked when 'Upgrade Range' button on the bottom bar is clicked
        /// </summary>
        public void UpgradeCurrentChefPath2()
        {
            var chef = currentChef.GetComponent<UpgradeTracker>();
            if (chef.getPath2Status() != 4 && upgradeManager.checkSufficientFunds(upgradeManager.getChefCost()))
            {
                CreditManager creditManager = GameObject.FindGameObjectWithTag("Credits").GetComponent<CreditManager>();
                creditManager.SpendCredits(upgradeManager.getChefCost());
                currentChef.GetComponent<UpgradeTracker>().UpgradePath2();
            }
        }

        /// <summary>
        /// Sells the current chef
        /// </summary>
        /// <remarks>Author: Ben</remarks>
        public void SellChef()
        {            
            // Destroy the chef
            Destroy(currentChef);

            // Close all UI (as chef no longer exists)
            upgradeRangeUI.SetActive(false);
            upgradeSpecialUI.SetActive(false);
            sellChefUI.SetActive(false);

            // Give back percentage of chef cost
            CreditManager creditManager = GameObject.FindGameObjectWithTag("Credits").GetComponent<CreditManager>();
            creditManager.IncreaseMoney(getSellCost());
        }

        private int getSellCost(){
            // Get chef/upgrade costs
            ShopSlotManager chefShopSlotManager = currentChef.GetComponent<ShopSlotManager>();
            
            int totalChefCost = 0;

            for(int i = 1; i < chefShopSlotManager.currentChefTier; i++){    // UpgradeCosts
                totalChefCost += chefShopSlotManager.chefCosts[i];
            }

            if(chefShopSlotManager.currentChefTier == 0){    // Initial buy cost
                totalChefCost = chefShopSlotManager.chefCosts[0];
            }

            return (int)(sellChefFraction * totalChefCost);
        }

        public GameObject[] GetPrepCooks()
        {
            return prepCookUpgrades;
        }

        public GameObject[] GetGrillardins()
        {
            return grillardinUpgrades;
        }

        public GameObject[] GetWaiters()
        {
            return waiterUpgrades;
        }

        public GameObject[] GetEntremetiers()
        {
            return entremetierUpgrades;
        }

    }
}