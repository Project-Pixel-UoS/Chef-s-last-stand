using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditManager : MonoBehaviour
{
    private int credits;
    [SerializeField] private Text text;
    
    void Start()
    {
        //some arbitrary number for now.
        credits = 50;
        text.text = "Credits: " + credits;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseMoney(int amount)
    {
        credits += amount;
        text.text = "Credits: " + credits;
    }

    public bool SpendCredits(int amount)
    {
        if (credits >= amount)
        {
            credits -= amount;
            text.text = "Credits: " + credits;
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
