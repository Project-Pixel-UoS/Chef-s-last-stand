using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private int cost;

    public int getCost(){
        return cost;
    }

    public void setCost(int newCost){
        cost = newCost;
    }
}
