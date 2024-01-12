using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the credits spending in the game.
/// </summary>
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

    /// <summary>
    /// Increases the credit amount.
    /// </summary>
    /// <param name="amount">The amount to increase credits by.</param>
    public void IncreaseMoney(int amount)
    {
        credits += amount;
        text.text = "Credits: " + credits;
    }

    /// <summary>
    /// Decreases the credit amount.
    /// </summary>
    /// <param name="amount">The amount to decrease credits by.</param>
    /// <returns>True if credits decreases, false otherwise</returns>
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
