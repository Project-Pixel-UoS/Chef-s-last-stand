using System;
using System.Collections;
using System.Collections.Generic;
using Projectile;
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
    private Vector3 mousePosition;

    private SpriteRenderer sprite;

    private void Start()
    {
        stats = gameObject.GetComponent<MouseStats>();
        credits = GameObject.FindGameObjectWithTag("Credits");
        creditsManager = credits.GetComponent<CreditManager>();
        sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DamageFactor damageFactor = HandleArmouredMouse(other);
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
    public IEnumerator TakeDamage(DamageFactor damageFactor)
    {
        float durationRemaining = damageFactor.damageDuration;
        while (durationRemaining > 0) //take damage until long lasting effect runs out
        {
            stats.health -= damageFactor.damage;
            durationRemaining -= damageFactor.damageRate;
            if(damageFactor.damage != 0) StartCoroutine(flashRed());



            if (stats.health <= 0)
            {
               
                HandleTrenchCoatMouse();
                Destroy(gameObject); //check for death
                creditsManager.IncreaseMoney(currencyAmount); //get money per kill
            }

            yield return new WaitForSeconds(damageFactor.damageRate);
        }
    }

    /// <summary>
    /// Make mouse flash red when damage is delt.
    /// </summary>
    /// <remarks>Author: Emily</remarks>

    public IEnumerator flashRed(){

        
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color= Color.white;


    }

    //if the mouse that died was trench coat, grab its death position and spawn more mice.
    private void HandleTrenchCoatMouse()
    {
        if (stats.canSplit)
        {
            int index = GetComponent<SpriteMove>().GetIndex();
            mousePosition = transform.position;
            LevelManager.LM.SplitMouse(mousePosition, index);
        }
    }

    //if an armoured mouse is hit by onions or knives, do no damage.
    private DamageFactor HandleArmouredMouse(Collision2D other)
    {
        if (stats.armoured && other.gameObject.GetComponent<SlownessProjectile>() == null) return gameObject.AddComponent<DamageFactor>();

        return other.gameObject.GetComponent<DamageFactor>();
    }
}