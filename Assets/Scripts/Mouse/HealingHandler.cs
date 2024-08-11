using System.Collections;
using UnityEngine;

namespace Mouse
{
    public class HealingHandler:MonoBehaviour
    {
        
        
        
        private SpriteRenderer sprite;
        private MouseStats stats;


        private void Awake()
        {
            sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
            stats = gameObject.GetComponent<MouseStats>();
        }
        
        public void Heal()
        {
            stats.health += 1;
            StopRedFlash();
            StartCoroutine(FlashGreen());
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

     
    }
}