using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chef.Upgrades;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using Util;


namespace Chef
{
    public class AbilityProjectile : MonoBehaviour
    {
        [SerializeField] private GameObject Projectile; // projectile for chef to shoot
        [SerializeField] private float cooldown; // time in between chef shooting (seconds)
        private float cooldownTimer; // timer for cooldown in between shots
        private Range range;
        private float originalSpeed;
        private UpgradeTracker upgradeTracker;
        private int projectilesThrown;


        private void Awake()
        {
            range = GetComponent<Range>();
            originalSpeed = Projectile.GetComponent<ProjectileMover>().projectileSpeed;
            upgradeTracker = GetComponent<UpgradeTracker>();
        }

        /// <summary> Update variable if buff added </summary>
        /// <remarks>Maintained by: Lishan Xu</remarks>
        void Update()
        {
            if (Projectile == null) return;
            GameObject furthestMouse = GetFurthestMouseInRange();
            if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;

            if (furthestMouse == null) return;
            Rotate(furthestMouse);
            Shoot();
        }

        /// <returns> find an arbitrary mouse that is in range </returns>
        /// <remarks>Maintained by: Antosh </remarks>
        private GameObject GetFurthestMouseInRange()
        {
            List<GameObject> mice = gameObject.GetComponent<Range>().GetMiceInRange();
            if (mice.Count > 0)
            {
                return mice.OrderByDescending(mouse => mouse.GetComponent<SpriteMove>().totalDistanceMoved).First();
            }

            return null;
        }

        /// <summary> Spins chef so that he is facing the mouse </summary>
        /// <param name = "furthestMouse"> mouse which chef will point towards</param>
        /// <remarks>Maintained by: Antosh Nikolak</remarks>
        private void Rotate(GameObject furthestMouse)
        {
            // Get coordinate dierction
            Vector3 direction = furthestMouse.transform.position - transform.position;

            // Calcualte angle as a quaternion
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
            GameObject p = Instantiate(Projectile, transform.position, transform.rotation);
            DamageFactor df = p.GetComponent<DamageFactor>();
            Buff buff = GetComponent<Buff>();
            if (buff != null)
            {
                p.GetComponent<DamageFactor>().damage = df.damage * buff.damageIncrease;
                p.GetComponent<ProjectileMover>().projectileSpeed = originalSpeed * buff.speedIncrease;
            }
        }
        
        //spawn another knife for the prep cook max ugprade.
        private IEnumerator HandleMaxPrepCook()
        {
            if(gameObject.tag.Equals("PrepCook") && upgradeTracker.getPath2Status() == 4)
            { 
                yield return new WaitForSeconds(0.1f);
                SpawnProjectile();
            }
        }
    }
}