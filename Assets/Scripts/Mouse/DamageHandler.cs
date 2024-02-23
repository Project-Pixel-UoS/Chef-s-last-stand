using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Designed to be attached to mice.
/// Responsible for health of each mice.
/// </summary>
/// <remarks>Author: Antosh</remarks>
public class DamageHandler : MonoBehaviour
{
    private MouseStats stats;
    [SerializeField] private int currencyAmount;
    private Coroutine damageCoroutine;
    private GameObject credits;
    private CreditManager creditsManager;
    private LevelManager levelManager;
    private SpriteMove moveManager;
    private Vector3 mousePosition;

    private void Start()
    {
        stats = gameObject.GetComponent<MouseStats>();
        credits = GameObject.FindGameObjectWithTag("Credits");
        levelManager = GameObject.Find("Level").GetComponent<LevelManager>();
        creditsManager = credits.GetComponent<CreditManager>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DamageFactor damageFactor = other.gameObject.GetComponent<DamageFactor>();
        if (damageCoroutine != null) //check mouse still taking poisonous damage
        {
            StopCoroutine(damageCoroutine);
        }
        damageCoroutine = StartCoroutine(TakeDamage(damageFactor));
    }
    

    /// <summary>
    /// Deal damage, instantaneously or over time if in bound projectile has long lasting effects
    /// </summary>
    /// <param name="damageFactor">container for projectile's damage stats</param>
    /// <remarks>Author: Antosh</remarks>
    IEnumerator TakeDamage(DamageFactor damageFactor)
    {
        float durationRemaining = damageFactor.damageDuration;
        while (durationRemaining > 0) //take damage until long lasting effect runs out
        {
            stats.health -= damageFactor.damage;
            durationRemaining -= damageFactor.damageRate;


            if (stats.health <= 0)
            {
                //if the mouse that died was trenchcoat, spawn more mice.
                if (stats.canSplit)
                {
                    int index = GetComponent<SpriteMove>().GetIndex();
                    mousePosition = transform.position;
                    levelManager.SplitMouse(mousePosition, index);
                }
                Destroy(gameObject); //check for death
                //get money per kill
                creditsManager.IncreaseMoney(currencyAmount);
            }

            yield return new WaitForSeconds(damageFactor.damageRate);
        }
    }
}