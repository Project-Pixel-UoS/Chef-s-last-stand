using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Shop
{
    /// <summary>
    /// Attached to every chef prefab
    /// </summary>
    /// <remarks>maintained by Antosh</remarks>
    public class PurchasedChef : MonoBehaviour
    {
        public int abilityUpgradeCost;
        public int rangeUpgradeCost;

        public String abilityDescription;

        public int specialTotal; // the cumulative cost of special abilities
        private int totalRefund; //(sellPrice + amount of money spent on range) * a percentage

        private CreditManager creditsManager;

        private void Start()
        {
            creditsManager = GameObject.FindGameObjectWithTag("Credits").GetComponent<CreditManager>();
        }


        /// <summary>
        /// If player has sufficient funds transaction is performed.
        /// </summary>
        /// <remarks>maintainer: Antosh</remarks>
        public void HandleChefTransaction()
        {
            creditsManager.SpendCredits(abilityUpgradeCost);
        }

        public bool CheckSufficientChefFunds()
        {
            return creditsManager.GetCredits() >= abilityUpgradeCost;
        }

        public bool HandleRangeTransaction()
        {
            bool sufficientFunds = creditsManager.SpendCredits(rangeUpgradeCost);
            return sufficientFunds;
        }

        public bool CheckSufficientRangeFunds()
        {
            return creditsManager.GetCredits() >= rangeUpgradeCost;
        }

        public void HandleChefRefund()
        {
            print(totalRefund);
            creditsManager.IncreaseMoney(totalRefund);
        }

        public void SetRefundPrice(int price)
        {
            double percentage = 0.5f;
            totalRefund = (int)(price * percentage);
        }

        public int GetRefundPrice()
        {
            return totalRefund;
        }
    }
}