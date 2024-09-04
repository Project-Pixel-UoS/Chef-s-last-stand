using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefPricing : MonoBehaviour
{

    [SerializeField] private int[] chefCosts;
    [SerializeField] private int rangeCost;

    public int[] getChefCosts(){
        return chefCosts;
    }

    public int getRangeCost(){
        return rangeCost;
    }
}
