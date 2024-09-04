using System;
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
        // represent cost of buying the chef if attached to side bar slot, or upgrading path 2 of the chef if attached
        // to chef game object
        public int[] chefCosts;
        public int rangeCost;
        private CreditManager creditsManager;
        [SerializeField] public int currentChefTier;
        
        private void Start()
        {
            loadCosts();
            chefImage = GetComponent<Image>();
            slotImage = transform.parent.GetComponent<Image>();
            creditsManager = GameObject.FindGameObjectWithTag("Credits").GetComponent<CreditManager>();
        }
        
        private void Update()
        {
            if (slotImage != null)
            {
                if (chefCosts[currentChefTier] > creditsManager.GetCredits())
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
        }

        private void loadCosts(){
            try{
                chefCosts = GetComponent<ChefPricing>().getChefCosts();
                rangeCost = GetComponent<ChefPricing>().getRangeCost();
            } catch{
                GameObject chef = GetComponent<DragChef>().getChef();
                chefCosts = chef.GetComponent<ChefPricing>().getChefCosts();
                rangeCost = chef.GetComponent<ChefPricing>().getRangeCost();
            }
        }

        public int getChefCost(){
            loadCosts();
            return chefCosts[currentChefTier];
        }

        public int getRangeCost(){
            loadCosts();
            return rangeCost;
        }

        public bool checkSufficientFunds(int cost){
            return cost <= creditsManager.GetCredits();
        }

    }
}