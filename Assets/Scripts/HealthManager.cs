using System;
using System.Collections;
using System.Collections.Generic;
using GameManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f; // The total health amount
    

    /// <summary> Reduces the health bar's health </summary>
    /// <param name = "damage"> The amount of health the health bar will lose</param>
    /// <remarks> Maintained by: Emily Johnston</remarks>
    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
        if (healthAmount <= 0)
        {
            GameManager.gameManager.GameOver();
        }
    }   
    



}
