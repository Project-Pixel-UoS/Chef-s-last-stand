using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the credits spending in the game.
/// </summary>
public class CreditManager : MonoBehaviour
{
    [SerializeField] private int credits;
    [SerializeField] private Text text;

    void Start()
    {
        text.text = "$" + credits;
    }


    /// <summary>
    /// Increases the credit amount.
    /// </summary>
    /// <param name="amount">The amount to increase credits by.</param>
    public void IncreaseMoney(int amount)
    {
        credits += amount;
        text.text = "$" + credits;
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
            text.text = "$" + credits;
            return true;
        }
        
        return false;
    }

    public int GetCredits()
    {
        return credits;
    }
}