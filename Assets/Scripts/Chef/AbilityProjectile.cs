using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chef.Upgrades;
using Mouse;
using Range;
using UnityEngine;
using Util;

namespace Chef
{
    public class AbilityProjectile : MonoBehaviour
    {
        [SerializeField] private GameObject projectile; // projectile for chef to shoot
        [SerializeField] private float cooldown; // time in between chef shooting (seconds)
        private float cooldownTimer; // timer for cooldown in between shots
        private UpgradeTracker upgradeTracker;
        private int projectilesThrown;
        private Buff buff;

        private void Awake()
        {
            upgradeTracker = GetComponent<UpgradeTracker>();
            buff = GetComponent<Buff>();
        }

        /// <summary> Update variable if buff added </summary>
        /// <remarks>Maintained by: Lishan Xu</remarks>
        void Update()
        {
            if (projectile == null) return;
            GameObject furthestMouse = gameObject.GetComponent<ChefRange>().GetFurthestVisibleMouseInRange();
            if (cooldownTimer > 0) cooldownTimer -= (Time.deltaTime / buff.ReloadTimeMultiplier);
            if (furthestMouse == null) return;
            Rotate(furthestMouse);
            Shoot();
        }

        /// <summary> Spins chef so that he is facing the mouse </summary>
        /// <param name = "furthestMouse"> mouse which chef will point towards</param>
        /// <remarks>Maintained by: Antosh Nikolak</remarks>
        private void Rotate(GameObject furthestMouse)
        {
            // Get coordinate direction
            Vector3 direction = furthestMouse.transform.position - transform.position;

            // Calculate angle as a quaternion
            float radians = Mathf.Atan2(direction.x, direction.y) * -1;
            float degrees = radians * Mathf.Rad2Deg;
            Quaternion target = Quaternion.Euler(0, 0, degrees);

            // Set rotation
            transform.rotation = target;
        }

        /// <summary> Shoot projectile in direction facing </summary>
        /// <remarks>
        /// Maintained by: Ben Brixton 
        /// Refactored by: Lishan Xu
        /// </remarks>
        private void Shoot()
        {
            if (cooldownTimer > 0) return;
            cooldownTimer = cooldown;
            Utils.PlayShootSound(gameObject);
            SpawnProjectile();
            StartCoroutine(HandleMaxPrepCook());
        }

        /// <summary>
        /// spawn the projectile that will be shot out.
        /// </summary>
        private void SpawnProjectile()
        {
            GameObject p = Instantiate(projectile, transform.position, transform.rotation);
            DamageFactor df = p.GetComponent<DamageFactor>();
            p.GetComponent<DamageFactor>().damage = df.damage * buff.DamageMultiplier;
        }

        //spawn another knife for the prep cook max upgrade.
        private IEnumerator HandleMaxPrepCook()
        {
            if (gameObject.tag.Equals("PrepCook") && upgradeTracker.getPath2Status() == 4)
            {
                yield return new WaitForSeconds(0.1f);
                SpawnProjectile();
            }
        }
    }
}