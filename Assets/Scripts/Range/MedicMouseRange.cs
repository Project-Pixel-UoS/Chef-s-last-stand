using System;
using System.Collections;
using System.Collections.Generic;
using Mouse;
using UnityEngine;

namespace Range
{
    
    //the bug is because a circle collider of radius 1 isnt the same size as my image of local scale 1. Units must be lines up.
    public class MedicMouseRange : Range
    {
        private Transform ring;
        private float healingRingRadius;
        private List<GameObject> miceHealedInCurrentPulse = new ();
        private const int healingPower = 10;

        private void Awake()
        {
            ring = transform.Find("Pulse");
            radius = 3; // serves as the max radius
        }

        private void Update()
        {
            UpdateHealingRing();
            HealMice();
        }

        private void UpdateHealingRing()
        {
            healingRingRadius += 0.01f;
            if (healingRingRadius > radius)
            {
                healingRingRadius = 0;
                miceHealedInCurrentPulse.Clear();
            }
            ring.localScale = new Vector3(healingRingRadius, healingRingRadius);
        }

        private void HealMice()
        {
            foreach (GameObject mouse in GetMiceInRange(healingRingRadius))
            {
                if (miceHealedInCurrentPulse.Contains(mouse) || mouse == gameObject) continue;
                mouse.GetComponent<MouseHealthHandler>().Heal(healingPower);
                miceHealedInCurrentPulse.Add(mouse);
            }
        }
    }
}