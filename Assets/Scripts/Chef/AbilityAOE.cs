using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;
using Range = Chef.Range;

public class AbilityAOE : MonoBehaviour
{
    private Range range;
    [SerializeField] private float cooldown; // time in between chef shooting (seconds)
    private float cooldownTimer; // timer for cooldown in between shots
    private DamageFactor damageFactor; // damage factor
    [SerializeField] private ParticleSystem fireParticles; // fire particles 
    [SerializeField] private int arcAngle;//angle of spread of fire in degrees

    void Start()
    {
        range = GetComponent<Range>();
        damageFactor = GetComponent<DamageFactor>(); // Get damage factor component
        
        var shape = fireParticles.shape;
        shape.arc = arcAngle;

        fireParticles.transform.eulerAngles = new Vector3(0, 0, 225 + (90f - arcAngle) / 2);
    }

    void Update()
    {
        GameObject furthestMouse = GetFurthestMouseInRange();
        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime; // Decrease cooldown
        AOE(); // Deal AOE damage
        if (furthestMouse == null) return;
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


    /// <returns> find an arbitrary mouse that is in range </returns>
    /// <remarks>Maintained by: Antosh </remarks>
    private GameObject GetFurthestMouseInRange()
    {
        List<GameObject> mice = range.GetMiceInRange();
        if (mice.Count > 0)
        {
            return mice.OrderByDescending(mouse => mouse.GetComponent<SpriteMove>().totalDistanceMoved).First();
        }

        return null;
    }



    /// <summary> Hits all mice in range, using DamageFactor component </summary>
    /// <remarks> Maintained by: Ben Brixton </remarks>
    private void AOE()
    {
        if (cooldownTimer > 0) return;
        
        List<GameObject> miceInRange = range.GetMiceInRange();
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
        int counter = 0;
        foreach (GameObject mouse in miceInRange)
        {
            Vector3 spriteDirection = transform.up; //  forward vector of the sprite
            Vector3 distance = (mouse.transform.position - transform.position);
            double mouseAngle = Vector3.Angle(spriteDirection, distance); // angle between mouse and chef
            float upperBound = arcAngle / 2f + 5f;
            if (mouseAngle < upperBound ) // check is angled within half the arc length from where chef is facing
            {
                
                //play particle effects and damage mouse
                StartCoroutine(mouse.GetComponent<DamageHandler>().TakeDamage(damageFactor));
                counter++;
    
            }
        }
    }

    private IEnumerator ManageParticles(List<GameObject> miceInRange)
    {
        if (miceInRange.Count > 0 && !fireParticles.isPlaying)
        {
            fireParticles.Play();
            //because fire particle system takes a while to fully activate we must wait before allowing to deal damage
            yield return new WaitForSeconds(0.2f);
        }
        else
        {
            fireParticles.Stop();
        }
    }
}