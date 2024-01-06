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
    [SerializeField] private int health;

    private Coroutine damageCoroutine;

    private void OnCollisionEnter2D(Collision2D other)
    {
        DamageFactor damageFactor = other.gameObject.GetComponent<DamageFactor>();
        if (damageCoroutine != null) //check mouse still taking poisonous damage
        {
            StopCoroutine(damageCoroutine);
        }
        damageCoroutine = StartCoroutine(TakeDamage(damageFactor));
    }

    //REMME+EMBER TO ADD BALLOONS FOR BEN
    //NOTICED BUG WITH COOLDOWN, when chef hits mouse, and then another mouse comes, it still takes 2 seconds

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
            health -= damageFactor.damage;
            durationRemaining -= damageFactor.damageRate;


            if (health <= 0) Destroy(gameObject); //check for death

            yield return new WaitForSeconds(damageFactor.damageRate);
        }
    }
}