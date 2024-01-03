using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditManager : MonoBehaviour
{
    public int credits;
    
    void Start()
    {
        //some arbitrary number for now.
        credits = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseMoney(int amount)
    {
        credits += amount;
    }

    public bool spendCredits(int amount)
    {
        if (credits >= amount)
        {
            credits -= amount;
            return true;
        }
        else
        {
            //console log for now.
            Debug.Log("no money");
            return false;
        }
    }
}
