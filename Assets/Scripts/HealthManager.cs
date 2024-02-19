using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f; // The total health amount
    
    [SerializeField] private GameObject gameOverScreen;



    /// <summary> Reduces the health bar's health </summary>
    /// <param name = "damage"> The amount of health the health bar will lose</param>
    /// <remarks> Maintained by: Emily Johnston</remarks>
    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
        if (healthAmount <= 0)
        {
            GameOver();
        }
    }

    
    /// <summary>
    /// Game Over Screen is shown, current wave with run until completion
    /// </summary>
    private void GameOver()
    {
        gameOverScreen.SetActive(true);
    }


    /// <returns>true if game over screen is showing</returns>
    public bool IsGameOver()
    {
        return gameOverScreen.activeSelf;
    }

    public void StartAgain()
    {
        SceneManager.LoadScene("SampleScene"); //Load scene called Game
    }
    
    



}
