
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
namespace Shop
{
    
    /// <summary>
    /// Attaches to the chef item displayed on the side bar
    /// </summary>
    /// <remarks>maintained by Antosh</remarks>
    public class ShopSlotManager : MonoBehaviour
    {
        
        private SpriteRenderer rangeImage;
        private Image chefImage;

        // represent cost of buying the chef if attached to side bar slot, or upgrading path 2 of the chef if attached
        // to chef game object
        public int chefCost; 
        public int rangeCost;
        public int specialTotal; // the cumulative cost of special abilities
        public String abilityDescription;
        private int totalRefund; //(sellPrice + amount of money spent on range) * a percentage = credits refunded
        private CreditManager creditsManager;
        
        private void Start()
        {
            chefImage = transform.GetChild(1).GetComponent<Image>();
            rangeImage = transform.GetChild(0).GetComponent<SpriteRenderer>();
            creditsManager = GameObject.FindGameObjectWithTag("Credits").GetComponent<CreditManager>();
        }
        
        private void Update()
        {
            if (rangeImage != null && chefImage != null)
            {
                if (chefCost > creditsManager.GetCredits())
                {
                    chefImage.color = UnityEngine.Color.red;
                    rangeImage.color = UnityEngine.Color.red;
                }
                else
                {
                    chefImage.color = UnityEngine.Color.white;
                }
            }
        }

        /// <summary>
        /// If player has sufficient funds transaction is performed.
        /// </summary>
        /// <remarks>maintainer: Antosh</remarks>
        public void HandleChefTransaction()
        {
            creditsManager.SpendCredits(chefCost);
        }

        public bool CheckSufficientChefFunds()
        {
            return creditsManager.GetCredits() >= chefCost;
        }

        public bool HandleRangeTransaction()
        {
            bool sufficientFunds = creditsManager.SpendCredits(rangeCost);
            return sufficientFunds;
        }
        
        public bool CheckSufficientRangeFunds()
        {
            return creditsManager.GetCredits() >= rangeCost;
        }

        public void HandleChefRefund()
        {
            print(totalRefund);
            creditsManager.IncreaseMoney(totalRefund);
        }

        public void SetRefundPrice(int price)
        {
            double percentage = 0.5f;
            totalRefund = (int)(price*percentage);
        }

        public int GetRefundPrice()
        {
            return totalRefund;
        }
    }
}