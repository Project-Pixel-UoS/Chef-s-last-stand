using System;
using System.Collections;
using System.Collections.Generic;
using Mouse;
using UnityEngine;

namespace Range
{
    
    public class MedicMouseRange : Range
    {
        private Transform ring;
        private float healingRingRadius;
        private List<GameObject> miceHealedInCurrentPulse = new ();
        private const int healingPower = 2;

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
            healingRingRadius += 0.6f * Time.deltaTime;
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