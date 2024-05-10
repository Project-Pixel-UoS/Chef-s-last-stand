using System;
using System.Collections;
using System.Collections.Generic;
using Projectile;
using UnityEditor;
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
    [SerializeField] private ParticleSystem onFire;
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
        DamageFactor damageFactor = HandleArmouredMouse(other.gameObject);
        if (damageCoroutine != null) //check mouse still taking poisonous damage
        {
            StopCoroutine(damageCoroutine);
        }
        damageCoroutine = StartCoroutine(TakeDamage(damageFactor));
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
 
        DamageFactor damageFactor = HandleArmouredMouse(other.gameObject);
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
        if (IsBurning(damageFactor)) { onFire.Play(); }
        HandleBurnChain(damageFactor);
        while (durationRemaining > 0) //take damage until long lasting effect runs out
        {
            stats.health -= damageFactor.damage;
            durationRemaining -= damageFactor.damageRate;

            if (stats.health <= 0)
            {
                HandleTrenchCoatMouse();
                creditsManager.IncreaseMoney(currencyAmount);//get money per kill
                try
                {
                    Destroy(gameObject); //check for death
                }
                catch
                {
                    break;
                }
            }
            if (damageFactor.damage != 0) StartCoroutine(flashRed());
            yield return new WaitForSeconds(damageFactor.damageRate);
        }
        if(onFire != null) { onFire.Stop(); }
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

    /// <summary>
    /// if an armoured mouse is hit by onions or knives, do no damage.
    /// </summary>
    /// <param name="collision">the collision object it collided with</param>
    /// <returns></returns>
    private DamageFactor HandleArmouredMouse(GameObject gameObject)
    {
        if (stats.armoured && gameObject.GetComponent<SlownessProjectile>() == null) return gameObject.AddComponent<DamageFactor>();

        return gameObject.GetComponent<DamageFactor>();
    }

    private bool IsBurning(DamageFactor damageFactor)
    {
        var currChef = damageFactor.chef.name;
        return currChef.Equals("Chef Grillardin 3(Clone)") || currChef.Equals("Chef Grillardin 4(Clone)");
    }

    public bool IsBurning()
    {
        return onFire.isPlaying;
    }
    private void HandleBurnChain(DamageFactor damageFactor)
    {
        if(damageFactor.chef.name.Equals("Chef Grillardin 4(Clone)"))
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
            if (colliders != null)
            {
                foreach (Collider2D collider in colliders)
                {
                    GameObject mouse = collider.gameObject;
                    if (mouse.CompareTag("Mouse") && mouse != gameObject)
                    {
                        Debug.Log("burning");
                        mouse.GetComponent<DamageHandler>().TakeDamage(damageFactor);
                    }
                }
            }
        }
    }
}