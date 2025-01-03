using System;
using Music;
using Range;
using Shop;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


namespace Chef.Upgrades
{
    /// <summary>
    /// class tracks how far along each upgrade path the chef is
    /// </summary>
    public class UpgradeTracker : MonoBehaviour
    {
        private int path1Status, path2Status;
        private int maxLevel1 = 4;
        private int maxLevel2 = 4;
        [SerializeField] Sprite[] progressBars;

        /// <summary>
        /// invoked upon Upgrade Range button on the bottom bar is clicked
        /// </summary>
        public void UpgradeRange()
        {
            if (path1Status == maxLevel1) return; // if path is upgraded to the max we can not upgrade it again
            SoundPlayer.instance.PlayUpgradeFX();
            path1Status++;
            GetComponent<ChefRange>().Radius *= 1.1f;
            GetComponent<ChefRange>().RadiusWithoutBuff *= 1.1f;
            
            RefreshRangeBar();
            RefreshSellChefBar();
        }

        /// <summary>
        /// invoked upon Upgrade Special button on the bottom bar is clicked
        /// </summary>
        public void UpgradeAbility()
        {
            if (path2Status == maxLevel2) return;
            SoundPlayer.instance.PlayUpgradeFX();
            path2Status++;
            var chefs = GetChefUpgrades();
            //instantiate new chef
            var chefParent = GameObject.FindGameObjectWithTag("ChefContainer");
            GameObject newChef = Instantiate(chefs[path2Status], transform.position, transform.rotation,
                chefParent.transform);
            DuplicateUpgradeStats(newChef);//change new chef stats
            var range = newChef.GetComponent<ChefRange>();
            range.RadiusWithoutBuff = GetComponent<ChefRange>().RadiusWithoutBuff; //copy range upgrade status over
            range.Radius = GetComponent<ChefRange>().Radius; //copy range upgrade status over
            range.EnableRangeRenderer(); //keep range active
            RefreshSpecialBar();
            RefreshSellChefBar();
            RefreshInfoWindow();
            ChefTracker.Instance.CurrentChef = newChef;
            Destroy(gameObject);
        }

        /// <summary>
        /// updates visual of range upgrade bar.
        /// </summary>
        public void RefreshRangeBar()
        {
            var rangeText = GameObject.FindGameObjectWithTag("RangeButtonText").GetComponent<Text>();
            if(path1Status == maxLevel1)
            {
                rangeText.text = "Range maxed";
            }
            else
            {
                var cost = transform.GetComponent<Shop.PurchasedChef>().rangeUpgradeCost;
                rangeText.text = "Range \n" + "$" + cost;
            }
            var upgradeBar1 = GameObject.FindGameObjectWithTag("UpgradeBar1").GetComponent<Image>();
            upgradeBar1.sprite = progressBars[path1Status];
        }

        /// <summary>
        /// updates visual of special upgrade bar.
        /// </summary>
        public void RefreshSpecialBar()
        {   
            var specialText = GameObject.FindGameObjectWithTag("SpecialButtonText").GetComponent<Text>();
            if(path2Status == maxLevel2)
            {
                specialText.text = "Ability maxed";
            }
            else
            {
                var cost = transform.GetComponent<PurchasedChef>().abilityUpgradeCost;
                specialText.text = "Promote \n" + "$" + cost;
            }
            var upgradeBar2 = GameObject.FindGameObjectWithTag("UpgradeBar2").GetComponent<Image>();
            upgradeBar2.sprite = progressBars[path2Status];
        }

        public void RefreshSellChefBar()
        {
            var sellText = GameObject.FindGameObjectWithTag("SellButtonText").GetComponent<Text>();
            var purchasedChef = transform.GetComponent<PurchasedChef>();
            int price = purchasedChef.specialTotal;
            price += purchasedChef.rangeUpgradeCost * path1Status;
            purchasedChef.SetRefundPrice(price);
            sellText.text = "Sell Chef: \n" + "$" + purchasedChef.GetRefundPrice();
        }

        public void RefreshInfoWindow()
        {
            String abilityInfo = transform.GetComponent<PurchasedChef>().abilityDescription;
            var infoText = GameObject.FindGameObjectWithTag("InfoWindowText").GetComponent<Text>();
            infoText.text = abilityInfo;
        }
        /// <returns> an array of selected chef's upgrade prefabs</returns>
        public GameObject[] GetChefUpgrades()
        {
            if (transform.CompareTag("PrepCook"))
            {
                return ChefTracker.Instance.GetPrepCooks();
            }
            else if (transform.CompareTag("Grillardin"))
            {
                return ChefTracker.Instance.GetGrillardins();
            }
            else if (transform.CompareTag("HeadChef"))
            {
                return ChefTracker.Instance.GetHeadChefs();
            }
            else if (transform.CompareTag("Waiter"))
            {
                return ChefTracker.Instance.GetWaiters();
            }else if (transform.CompareTag("Potager"))
            {
                return ChefTracker.Instance.GetPotagers();
            }
            else if (transform.CompareTag("Entremetier"))
            {
                return ChefTracker.Instance.GetEntremetiers();
            }

            return null;
        }

        /// <summary>
        /// copies the current chef's upgrade statuses to the upgraded version.
        /// </summary>
        /// <param name="newChef">the upgraded chef to paste the statuses to.</param>
        public void DuplicateUpgradeStats(GameObject newChef)
        {
            var upgradeTracker = newChef.GetComponent<UpgradeTracker>();
            upgradeTracker.SetPath1Status(path1Status);
            upgradeTracker.SetPath2Status(path2Status);
        }

        public void SetPath1Status(int i)
        {
            path1Status = i;
        }

        public void SetPath2Status(int i)
        {
            path2Status = i;
        }

        public int getPath2Status()
        {
            return path2Status;
        }

        public int getPath1Status()
        {
            return path1Status;
        }
    }
}