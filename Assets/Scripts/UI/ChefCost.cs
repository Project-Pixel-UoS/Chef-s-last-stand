using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Shop;
public class ChefCost : MonoBehaviour
{
    [SerializeField] GameObject shopItem;
    TMPro.TextMeshProUGUI textObject;

    void Start()
    {
        textObject = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        GameObject chef = shopItem.GetComponent<DragChef>().getChef();
        int chefCost = chef.GetComponent<ShopSlotManager>().getChefCost();
        textObject.text = chefCost.ToString();
    }
}