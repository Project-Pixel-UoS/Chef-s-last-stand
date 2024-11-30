using System.Collections;
using UnityEngine;

namespace Mouse
{
    public class SlownessHandler : MonoBehaviour
    {
        private MouseStats stats;
        private bool isSlowed;
        private Coroutine speedRestoreCoroutine;
        private float originalSpeed;
        
        public GameObject gooper;



        private void Start()
        {
            stats = gameObject.GetComponent<MouseStats>();
            originalSpeed = stats.speed;
        }

        /// <summary> reduced speed of mouse </summary>
        /// <param name = "slownessProjectile"> stores slowness properties of projectile </param>
        /// <remarks> Maintained by: Antosh</remarks>
        private void SlowMouse(SlownessProjectile slownessProjectile)
        {
            if (isSlowed) //stop chef being doubly slowed
            {
                StopCoroutine(speedRestoreCoroutine);
                stats.speed = originalSpeed;
                isSlowed = false;
                SlowMouse(slownessProjectile);
            }
            else
            {
                isSlowed = true;
                stats.speed /= slownessProjectile.slownessFactor;
                speedRestoreCoroutine = StartCoroutine(RestoreSpeed(slownessProjectile)); //runs with delay
            }
        }

        //deprecated
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
            stats.speed = originalSpeed;
            isSlowed = false;
        }

        public void HandleSlownessProjectile(Collision2D other)
        {
            GameObject projectile = other.gameObject;
            SlownessProjectile slownessProjectile = projectile.GetComponent<SlownessProjectile>();
            if (slownessProjectile != null)
            {
                SlowMouse(slownessProjectile);
                if (CheckChefProducesGooper(slownessProjectile) && gooper == null)
                {
                    CreateGooper(slownessProjectile.gooper, slownessProjectile.duration);
                }
            }
        }

        private static bool CheckChefProducesGooper(SlownessProjectile projectile)
        {
            return projectile.gooper != null;
        }

        public void CreateGooper(GameObject gooperPrefab, float destroyDuration)
        {
            gooper = Instantiate(gooperPrefab, transform.position, transform.rotation);
            Destroy(gooper, destroyDuration);
        }

        public void DestroyGooperIfExists()
        {
            if (gooper != null)
            {
                Destroy(gooper);
            }
        }
    }
}