using System;
using System.Collections;
using UnityEngine;

namespace Mouse
{
    public class MouseHealthHandler:MonoBehaviour
    {
        private SpriteRenderer sprite;
        private float maxHealth;
        private float health;
        public float Health
        {
            get => health;
        }
        
        private void Start()
        {
            sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
            maxHealth = gameObject.GetComponent<MouseStats>().maxHealth;
            health = maxHealth;
        }


        public void Heal(int healingPower)
        {
            StopRedFlash();
            StartCoroutine(FlashGreen());
            if (health + healingPower <= maxHealth)
            {
                health += healingPower;
            }
           
        }

        private void StopRedFlash()
        {
            IEnumerator flashRedCoroutine = GetComponent<DamageHandler>().flashRedCoroutine;
            if (flashRedCoroutine != null)
            {
                StopCoroutine(flashRedCoroutine);
            }
        }

        public IEnumerator FlashGreen()
        {
            sprite.color = UnityEngine.Color.green;
            yield return new WaitForSeconds(0.1f);
            sprite.color = UnityEngine.Color.white;
        }

        public void DecrementHealth(float damage)
        {
            health -= damage;
        }



     
    }
}