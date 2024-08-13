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
        
        private void Awake()
        {
            sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
            maxHealth = gameObject.GetComponent<MouseStats>().maxHealth;
        }
        
        public void Heal(int healingPower)
        {
            Debug.Log("HEAL");
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
            sprite.color = Color.green;
            yield return new WaitForSeconds(0.1f);
            sprite.color = Color.white;
        }

        public void DecrementHealth(float damage)
        {
            health -= damage;
        }



     
    }
}