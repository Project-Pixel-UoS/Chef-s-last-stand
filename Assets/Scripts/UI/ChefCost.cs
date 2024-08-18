using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefCost : MonoBehaviour
{
    [SerializeField] GameObject shopItem;
    TMPro.TextMeshProUGUI textObject;

    void Start()
    {
        textObject = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        int cost = shopItem.GetComponent<DragChef>().getChef().GetComponent<ShopItem>().getCost();
        textObject.text = cost.ToString();
    }
}
