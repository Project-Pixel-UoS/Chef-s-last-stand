using Range;
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

        [SerializeField] private GameObject upgradeRangeUI; // game object containing upgrade button and upgrade bar
        [SerializeField] private GameObject upgradeSpecialUI;
        [SerializeField] private GameObject[] prepCookUpgrades;
        [SerializeField] private GameObject[] grillardinUpgrades;
        [SerializeField] private GameObject[] headChefUpgrades;
        [SerializeField] private GameObject[] waiterUpgrades;
        [SerializeField] private GameObject[] potagerUpgrades;
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
            DisableRange(currentChef);
            if (chef == null)
            {
                RemoveUpgradeButtons();
            }
            else
            {
                ShowRange(chef);
                DisplayUpgradeButtons(chef);
            }
            currentChef = chef;
        }

        private void ShowRange(GameObject chef)
        {
            chef.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        }

        private void DisableRange(GameObject chef)
        {
            if (chef != null)
            {
                chef.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        private void DisplayUpgradeButtons(GameObject chef)
        {
            upgradeRangeUI.SetActive(true);
            chef.GetComponent<UpgradeTracker>().RefreshRangeBar();
            upgradeSpecialUI.SetActive(true);
            chef.GetComponent<UpgradeTracker>().RefreshSpecialBar();
            upgradeManager = chef.GetComponent<ShopSlotManager>();
        }

        private void RemoveUpgradeButtons()
        {
            upgradeRangeUI.SetActive(false);
            upgradeSpecialUI.SetActive(false);
        }

        /// <summary>
        /// Invoked when 'Upgrade Range' button on the bottom bar is clicked
        /// </summary>
        public void UpgradeCurrentChefPath1()
        {
            var chef = currentChef.GetComponent<UpgradeTracker>();
            if (chef.getPath1Status() != 4 && upgradeManager.CheckSufficientRangeFunds())
            {
                upgradeManager.HandleRangeTransaction();
                currentChef.GetComponent<UpgradeTracker>().UpgradeRange();
            }
        }

        /// <summary>
        /// Invoked when 'Upgrade Range' button on the bottom bar is clicked
        /// </summary>
        public void UpgradeCurrentChefPath2()
        {
            var chef = currentChef.GetComponent<UpgradeTracker>();
            if (chef.getPath2Status() != 4 && upgradeManager.CheckSufficientChefFunds())
            {
                upgradeManager.HandleChefTransaction();
                currentChef.GetComponent<UpgradeTracker>().UpgradeAbility();
            }
        }

        /// <summary>
        /// invoked when a chef is pressed
        /// </summary>
        /// <param name="lastClickedChef">most recently pressed chef</param>
        public void OnChefClicked(GameObject lastClickedChef)
        {
            SelectNewChef(IsChefAlreadySelected(lastClickedChef) ? null : lastClickedChef);
        }

        private bool IsChefAlreadySelected(GameObject lastClickedChef)
        {
            return lastClickedChef == currentChef;
        }

        public GameObject[] GetPrepCooks()
        {
            return prepCookUpgrades;
        }
        public GameObject[] GetGrillardins()
        {
            return grillardinUpgrades;
        }

        public GameObject[] GetHeadChefs()
        {
            return headChefUpgrades;
        }

        public GameObject[] GetWaiters()
        {
            return waiterUpgrades;
        }
        public GameObject[] GetPotagers()
        {
            return potagerUpgrades;
        }
        
        public GameObject[] GetEntremetiers()
        {
            return entremetierUpgrades;
        }



    }
}