using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    /// <summary>
    /// Attached to the chef item displayed on the side bar
    /// </summary>
    /// <remarks>maintained by Antosh</remarks>
    public class ChefForSale : MonoBehaviour
    {
        private SpriteRenderer rangeImage;
        private Image chefImage;
        
        public int cost;

        private CreditManager creditsManager;
        
        private void Start()
        {
            rangeImage = transform.GetChild(0).GetComponent<SpriteRenderer>();
            chefImage = transform.GetChild(1).GetComponent<Image>();
            creditsManager = GameObject.FindGameObjectWithTag("Credits").GetComponent<CreditManager>();
        }
        
        private void Update()
        {
            if (rangeImage != null)
            {
                if (cost > creditsManager.GetCredits())
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
            creditsManager.SpendCredits(cost);
        }

        public bool CheckSufficientChefFunds()
        {
            return creditsManager.GetCredits() >= cost;
        }

    }
}