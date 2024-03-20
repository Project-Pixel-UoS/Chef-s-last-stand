using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class AbilityAOE : MonoBehaviour
{
    [SerializeField] private float range; // range at which chef can attack mice
    [SerializeField] private float cooldown; // time in between chef shooting (seconds)
    private float cooldownTimer; // timer for cooldown in between shots
    private DamageFactor damageFactor; // damage factor
    [SerializeField] private ParticleSystem fireParticles; // fire particles 
    [SerializeField] private int arcAngle;//angle of spread of fire in degrees

    void Start()
    {
        damageFactor = GetComponent<DamageFactor>(); // Get damage factor component
        // double arcLength = Math.PI / 180f * arcAngle * range;
        
        var shape = fireParticles.shape;
        shape.arc = arcAngle;

        fireParticles.transform.eulerAngles = new Vector3(0, 0, 225 + (90f - arcAngle) / 2);
        // fireParticles.emission.
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
        degrees = RotateBy180(degrees); //rotate chef 180 because his image is looking backwards
        Quaternion target = Quaternion.Euler(0, 0, degrees);
        transform.rotation = target;
    }

    private float RotateBy180(float degrees)
    {
        return degrees + ((degrees >= 0) ? 180 : -180);
    }


    /// <returns> find an arbitrary mouse that is in range </returns>
    /// <remarks>Maintained by: Antosh </remarks>
    private GameObject GetFurthestMouseInRange()
    {
        List<GameObject> mice = GetMiceInRange();
        if (mice.Count > 0)
        {
            return mice.OrderByDescending(mouse => mouse.GetComponent<SpriteMove>().totalDistanceMoved).First();
        }

        return null;
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
            }
        }

        return miceInRange;
    }

    /// <summary> Hits all mice in range, using DamageFactor component </summary>
    /// <remarks> Maintained by: Ben Brixton </remarks>
    private void AOE()
    {
        if (cooldownTimer > 0) return;
        
        List<GameObject> miceInRange= GetMiceInRange();
        Debug.Log("mice in range: " + miceInRange.Count);
        
        var particleEmission = fireParticles.emission;
        if (miceInRange.Count > 0)
        {
            particleEmission.enabled = true;
            fireParticles.Play();
        }
        else
        {
            // var particleEmission = fireParticles.emission;
            print("PAUSING");
            particleEmission.enabled = false;
            fireParticles.Stop(); 
        }

        foreach (GameObject mouse in miceInRange)
        {
            Vector3 spriteDirection = -transform.up; //  forward vector of the sprite
            Debug.Log("sprite direction: " + spriteDirection);
            Vector3 distance = (mouse.transform.position - transform.position);
            double mouseAngle = Vector3.Angle(spriteDirection, distance); // angle between mouse and chef
            print("Angle: " + mouseAngle);
          
            if (mouseAngle < arcAngle / 2f)  // check is angled within half the arc length from where chef is facing
            {
                //play particle effects and damage mouse

                StartCoroutine(mouse.GetComponent<DamageHandler>().TakeDamage(damageFactor));
            }
        }

        cooldownTimer = cooldown;
    }
}