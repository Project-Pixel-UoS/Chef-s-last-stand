using System;
using System.Collections.Generic;
using System.Linq;
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
        
        
        /// <summary> Spins chef so that he is facing the mouse </summary>
        /// <param name = "furthestMouse"> mouse which chef will point towards</param>
        /// <remarks>Maintained by: Antosh Nikolak</remarks>
        private void Rotate(GameObject furthestMouse)
        {
            Vector3 direction = furthestMouse.transform.position - transform.position;
            float radians = Mathf.Atan2(direction.x, direction.y) * -1;
            float degrees = radians * Mathf.Rad2Deg; // negative radians means chef has turned clock wise
            degrees = RotateBy180(degrees); //rotate chef 180 because his image is looking backwards
            Quaternion target = Quaternion.Euler(0, 0, degrees);
            transform.rotation = target;
        }

        private float RotateBy180(float degrees)
        {
            return degrees + ((degrees >= 0) ? 180 : -180);
        }



        private void Awake()
        {
            range = GetComponent<Range>();
        }


        void Update()
        {
            if (Projectile == null) return;
            GameObject furthestMouse = GetFurthestMouseInRange();
            if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;

            if (furthestMouse == null) return;
            Rotate(furthestMouse);
            Shoot();
        }


        /// <summary> Spins chef so that he is facing the mouse </summary>
        /// <param name = "furthestMouse"> mouse which chef will point towards</param>
        /// <remarks>Maintained by: Antosh Nikolak</remarks>
        // private void Rotate(GameObject furthestMouse)
        // {
        //     Vector3 direction = furthestMouse.transform.position - transform.position;
        //     float radians = Mathf.Atan2(direction.x, direction.y) * -1;
        //     float degrees = radians * Mathf.Rad2Deg;
        //     Quaternion target = Quaternion.Euler(0, 0, degrees);
        //     transform.rotation = target;
        // }


        /// <returns> find an arbitrary mouse that is in range </returns>
        /// <remarks>Maintained by: Antosh </remarks>
        private GameObject GetFurthestMouseInRange()
        {
            List<GameObject> mice = GetMiceInRange();
            if (mice.Count > 0)
            {
                return mice.OrderByDescending(mouse => mouse.GetComponent<SpriteMove>().totalDistanceMoved).First();
            }

            return null;
        }

        /// <returns>
        /// mice in range of the chef
        /// </returns>
        /// <remarks> maintained by: Antosh </remarks>
        private List<GameObject> GetMiceInRange()
        {
            var mice = GameObject.FindGameObjectsWithTag("Mouse");
            var miceInRange = new List<GameObject>();
            foreach (var mouse in mice)
            {
                float distance = (mouse.transform.position - transform.position).magnitude;
                if (distance <= range.radius)
                {
                    miceInRange.Add(mouse);
                }
            }

            return miceInRange;
        }

        /// <summary> Shoot projectile in direction facing </summary>
        /// <remarks>Maintained by: Ben Brixton </remarks>
        private void Shoot()
        {
            if (cooldownTimer > 0) return;
            cooldownTimer = cooldown;
            Instantiate(Projectile, transform.position, transform.rotation);
        }
    }
}