using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chef;
using Mouse;
using Range;
using UnityEngine;
using Util;

public class AbilityAOE : MonoBehaviour
{
    private ChefRange chefRange;
    [SerializeField] private float cooldown; // time in between chef shooting (seconds)
    private float cooldownTimer; // timer for cooldown in between shots
    private DamageFactor damageFactor; // damage factor
    [SerializeField] private ParticleSystem fireParticles; // fire particles 
    [SerializeField] private int arcAngle; //angle of spread of fire in degrees

    void Start()
    {
        chefRange = GetComponent<ChefRange>();
        damageFactor = GetComponent<DamageFactor>(); // Get damage factor component

        var shape = fireParticles.shape;
        shape.arc = arcAngle;
        
        AdjustParticleShootingDirection();

    }

    private void AdjustParticleShootingDirection()
    {
        fireParticles.transform.localEulerAngles = new Vector3(0, 0, (180 - arcAngle) / 2f);
    }

    void Update()
    {
        GameObject furthestMouse = chefRange.GetFurthestVisibleMouseInRange();
        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime; // Decrease cooldown
        if (furthestMouse == null)
        {
            StartCoroutine(StopParticles());
            return;
        }
        AOE(); // Deal AOE damage
        Rotate(furthestMouse);
    }

    /// <summary> Spins chef so that he is facing the mouse </summary>
    /// <param name = "furthestMouse"> mouse which chef will point towards</param>
    /// <remarks>Maintained by: Antosh Nikolak</remarks>
    private void Rotate(GameObject furthestMouse)
    {
        Vector3 direction = furthestMouse.transform.position - transform.position;
        float radians = Mathf.Atan2(direction.x, direction.y) * -1;
        float degrees = radians * Mathf.Rad2Deg;
        Quaternion target = Quaternion.Euler(0, 0, degrees);
        transform.rotation = target;
    }
    

    /// <summary> Hits all mice in range, using DamageFactor component </summary>
    /// <remarks> Maintained by: Ben Brixton </remarks>
    private void AOE()
    {
        if (cooldownTimer > 0) return;
        List<GameObject> miceInRange = chefRange.GetVisibleMiceInRange();
        StartCoroutine(DealDamage(miceInRange));
        cooldownTimer = cooldown;
    }

    //todo function has a bug where only 1 mouse gets damaged because function only looks at the mice position, not accounting for the width of his entire body - had no time to fix it
    /// <summary>
    /// Deals damage to each mice in the mice in range list
    /// </summary>
    /// <param name="miceInRange"> List of mice within chefs rangs</param>
    /// <remarks>Martin</remarks>
    private IEnumerator DealDamage(List<GameObject> miceInRange)
    {
        yield return ManageParticles(miceInRange);
        foreach (GameObject mouse in miceInRange)
        {
            if (mouse != null)
            {
                var mouseAngle = CalculateMouseAngle(mouse);
                float upperBound = arcAngle / 2f + 5f;
                if (mouseAngle < upperBound) // check is angled within half the arc length from where chef is facing
                {
                    //play particle effects and damage mouse
                    StartCoroutine(mouse.GetComponent<DamageHandler>().TakeDamage(damageFactor));
                }
            }
        }
    }

    private double CalculateMouseAngle(GameObject mouse)
    {
        Vector3 spriteDirection = transform.up; //  forward vector of the sprite
        Vector3 distance = (mouse.transform.position - transform.position);
        double mouseAngle = Vector3.Angle(spriteDirection, distance); // angle between mouse and chef
        return mouseAngle;
    }

    private IEnumerator ManageParticles(List<GameObject> miceInRange)
    {
        if (miceInRange.Count > 0 && !fireParticles.isPlaying)
        {
            Utils.PlayShootSound(gameObject);
            fireParticles.Play();
            //because fire particle system takes a while to fully activate we must wait before allowing to deal damage
            yield return new WaitForSeconds(0.2f);
        }
        else
        {
            yield return StopParticles();
        }
    }

    private IEnumerator StopParticles()
    {
        fireParticles.Stop();
        yield return new WaitForSeconds(0.7f);
        Utils.StopShootSound(gameObject);
    }

    public void SetFireDistance(float radius)
    {
        var main = GetComponentInChildren<ParticleSystem>().main;
        var emission = GetComponentInChildren<ParticleSystem>().emission;
        
        main.startSpeed =  (radius - 1) * 5;
        emission.rateOverTime = (radius - 1) * 200;
    }
}