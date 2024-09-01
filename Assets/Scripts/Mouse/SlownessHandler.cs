using System.Collections;
using UnityEngine;

namespace Mouse
{
    public class SlownessHandler : MonoBehaviour
    {
        private MouseStats stats;
        private bool isSlowed;
        private Coroutine speedRestoreCoroutine;


        private void Start()
        {
            stats = gameObject.GetComponent<MouseStats>();
        }

        /// <summary> reduced speed of mouse </summary>
        /// <param name = "slownessProjectile"> stores slowness properties of projectile </param>
        /// <remarks> Maintained by: Antosh</remarks>
        private void SlowMouse(SlownessProjectile slownessProjectile)
        {
            if (isSlowed) //stop chef being doubly slowed
            {
                RestartSlownessCoroutine(slownessProjectile);
            }
            else
            {
                isSlowed = true;
                stats.speed /= slownessProjectile.slownessFactor;
                speedRestoreCoroutine = StartCoroutine(RestoreSpeed(slownessProjectile)); //runs with delay
            }
        }

        private void RestartSlownessCoroutine(SlownessProjectile slownessProjectile)
        {
            StopCoroutine(speedRestoreCoroutine);
            speedRestoreCoroutine = StartCoroutine(RestoreSpeed(slownessProjectile));
        }

        /// <summary> restores speed back to normal after delay </summary>
        /// <param name = "slownessProjectile"> stores slowness properties of projectile </param>
        /// <remarks> Maintained by: Antosh</remarks>
        private IEnumerator RestoreSpeed(SlownessProjectile slownessProjectile)
        {
            yield return new WaitForSeconds(slownessProjectile.duration);
            stats.speed *= slownessProjectile.slownessFactor;
            isSlowed = false;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
             GameObject otherGameObject = other.gameObject;
             SlownessProjectile slownessProjectile = otherGameObject.GetComponent<SlownessProjectile>();
             if (slownessProjectile != null && (!isSlowed || !otherGameObject.name.Equals("Potager Projectile 4(Clone)")))
             {
                 SlowMouse(slownessProjectile);
             }

        }
    }
}