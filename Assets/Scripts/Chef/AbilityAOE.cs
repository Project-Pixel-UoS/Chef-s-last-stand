using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAOE : MonoBehaviour
{
    [SerializeField] private float range; // range at which chef can attack mice
    [SerializeField] private float cooldown; // time in between chef shooting (seconds)
    private float cooldownTimer; // timer for cooldown in between shots
    private DamageFactor damageFactor; // damage factor
    private ParticleSystem fireParticles; // fire particles 

    void Start(){
        damageFactor = GetComponent<DamageFactor>();        // Get damage factor component
        fireParticles = GetComponent<ParticleSystem>();     // Get fire particles component
    }

    void Update(){
        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;     // Decrease cooldown
        AOE();      // Deal AOE damage
    }


    /// <returns>
    /// mice in range of the chef
    /// </returns>
    /// <remarks> maintained by: Antosh </remarks>
    private List<GameObject> GetMiceInRange()
    {
        var mice = GameObject.FindGameObjectsWithTag("Mouse");
        var miceInRange = new List<GameObject>();
        foreach (var mouse in mice)
        {
            float distance = (mouse.transform.position - transform.position).magnitude;
            if (distance <= range)
            {
                miceInRange.Add(mouse);
                //play particle effects
                var particleEmission = fireParticles.emission;
                particleEmission.enabled = true;
                fireParticles.Play();
            }
        }

        return miceInRange;
    }

    /// <summary> Hits all mice in range, using DamageFactor component </summary>
    /// <remarks> Maintained by: Ben Brixton </remarks>
    private void AOE(){
        if(cooldownTimer > 0) return;

        foreach(GameObject mouse in GetMiceInRange()){
            StartCoroutine(mouse.GetComponent<DamageHandler>().TakeDamage(damageFactor));
        }

        cooldownTimer = cooldown;
    }


}
