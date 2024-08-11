using System;
using System.Collections;
using Mouse;
using UnityEngine;

namespace Range
{
    public class MedicMouseRange : Range
    {

       public GameObject ringEffect;
        
        private void Awake()
        {
            CreateRingEffectObject();
        }

        private void CreateRingEffectObject()
        {
            // create 
            ringEffect = Instantiate(ringEffect, transform);
        }

        

        private void Start()
        {
            SetRadius(5);
            StartCoroutine(InitializeMouseHealingPulse());
        }

        IEnumerator InitializeMouseHealingPulse()
        {
            while (true)
            {
                HealMice();
                yield return new WaitForSeconds(5);
            }
        }

        private void HealMice()
        {
            foreach (GameObject mouse in GetMiceInRange())
            {
                mouse.GetComponent<HealingHandler>().Heal();
            }
        }

        private void HealMouse(GameObject mouse)
        {
        }



    }
}