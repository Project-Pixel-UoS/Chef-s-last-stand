using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Range
{
    public class Range : MonoBehaviour
    {
        [SerializeField] protected float radius; // range at which chef can attack mice
        
        
        public float Radius
        {
            get => radius;
            set => SetRadius(value);
        }
        

        protected virtual void SetRadius(float radius)
        {
            this.radius = radius;
            var abilityAoe = gameObject.GetComponent<AbilityAOE>();
            if (abilityAoe != null) abilityAoe.SetFireDistance(radius);
        }
        
        public bool IsMouseInRange(GameObject mouse)
        {
            return GetMiceInRange().Contains(mouse);
        }
        

        public List<GameObject> GetMiceInRange()
        {
            return GetMiceInRange(Radius);
        }
        
        /// <returns>
        /// mice in range of the chef
        /// </returns>
        /// <remarks> maintained by: Antosh </remarks>
        protected List<GameObject> GetMiceInRange(float radius)
        {
            radius = Utils.ResizeRadiusOutsideCanvas(radius);
            var miceInRange = new List<GameObject>();
            var colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            if (colliders != null)
            {
                foreach (Collider2D collider in colliders)
                {
                    GameObject gameObject = collider.gameObject;
                    if (gameObject.CompareTag("Mouse"))
                    {
                        miceInRange.Add(gameObject);
                    }
                }
            }
            return miceInRange;

        }

    }
}