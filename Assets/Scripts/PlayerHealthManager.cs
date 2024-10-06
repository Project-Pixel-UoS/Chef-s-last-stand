using System;
using System.Collections;
using System.Collections.Generic;
using GameManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    public Image healthBar;
    private const float InitialHealth = 100f; // The total health amount
    public float currentHealth;

    private void Start()
    {
        currentHealth = InitialHealth;
    }


    /// <summary> Reduces the health bar's health </summary>
    /// <param name = "damage"> The amount of health the health bar will lose</param>
    /// <remarks> Maintained by: Emily Johnston</remarks>
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / 100f;
        if (currentHealth <= 0)
        {
            GameManager.gameManager.GameOver();
        }
    }

    public float GetCurrentHealthAsPercentage()
    {
        return 100 * currentHealth / InitialHealth;
    }
}
