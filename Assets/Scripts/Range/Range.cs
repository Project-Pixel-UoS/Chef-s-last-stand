using System.Collections.Generic;
using UnityEngine;

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
        }
        
        public bool IsMouseInRange(GameObject mouse)
        {
            return GetMiceInRange().Contains(mouse);
        }
        

        public List<GameObject> GetMiceInRange()
        {
            return GetMiceInRange(radius);
        }
        
        /// <returns>
        /// mice in range of the chef
        /// </returns>
        /// <remarks> maintained by: Antosh </remarks>
        protected List<GameObject> GetMiceInRange(float radius)
        {
            float ratio = 0;
            if (Screen.height > 1080)
            {
                ratio = 1080 / (float)Screen.height;
                radius *= ratio;
            }
            else if (Screen.width > 1920)
            {
                ratio = 1920 / (float)Screen.width;
                radius *= ratio;
            }
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