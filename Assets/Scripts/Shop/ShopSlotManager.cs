
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
        
        private Image slotImage;
        private Image chefImage;

        public int chefCost;
        private CreditManager creditsManager;
        
        private void Start()
        {
            chefImage = GetComponent<Image>();
            slotImage = transform.parent.GetComponent<Image>();
            creditsManager = GameObject.FindGameObjectWithTag("Credits").GetComponent<CreditManager>();
        }
        
        private void Update()
        {
            if (chefCost > creditsManager.GetCredits())
            {
                chefImage.color = Color.red;
                slotImage.color = Color.red;
            }
            else
            {
                chefImage.color = Color.white;
                slotImage.color = new Color(0.86f, 0.61f, 0.21f);
            }
        }

        /// <summary>
        /// If player has sufficient funds transaction is performed.
        /// </summary>
        /// <returns>true if enough there are sufficient funds</returns>
        /// <remarks>maintainer: Antosh</remarks>
        public bool HandleCreditTransaction()
        {
            bool sufficientFunds = creditsManager.SpendCredits(chefCost);
            return sufficientFunds;
        }
        

    }
}