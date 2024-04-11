using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace Chef
{
    public class AbilityProjectile : MonoBehaviour
    {
        [SerializeField] private GameObject Projectile; // projectile for chef to shoot
        [SerializeField] private float cooldown; // time in between chef shooting (seconds)
        private float cooldownTimer; // timer for cooldown in between shots
        private Range range;
        private float originalSpeed;



        private void Awake()
        {
            range = GetComponent<Range>();
            originalSpeed = Projectile.GetComponent<ProjectileMover>().projectileSpeed;
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
            GameObject p = Instantiate(Projectile, transform.position, transform.rotation);
            DamageFactor df = this.GetComponent<DamageFactor>();
            Buff bf = this.GetComponent<Buff>();
            if (bf != null)
            {
                p.GetComponent<DamageFactor>().damage = df.damage * bf.damageIncrease;
                p.GetComponent<ProjectileMover>().projectileSpeed = originalSpeed * bf.speedIncrease;
            }
        }
    }
}