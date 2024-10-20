using System.Collections.Generic;
using System.Linq;
using Mouse;
using UnityEngine;
namespace Range
{
    public class ChefRange : Range
    {
        [SerializeField] private GameObject rangeObject; //imports the chefs range.
        private SpriteRenderer rangeSpriteRenderer;
        
        public float RadiusWithoutBuff { get; set; }
        
        void Awake()
        {
            rangeSpriteRenderer = rangeObject.GetComponent<SpriteRenderer>();
            ResizeRangeVisual();
            rangeSpriteRenderer.color = Color.Color.rangeColor;
            rangeSpriteRenderer.enabled = false;
            RadiusWithoutBuff = Radius;
            

        }

        void Update()
        {
            HandleRangeBuff();
        }
        
        /// <returns> find an arbitrary mouse that is in range </returns>
        /// <remarks>Maintained by: Antosh </remarks>
        public GameObject GetFurthestMouseInRange()
        {
            List<GameObject> mice = GetMiceInRange();
            mice.RemoveAll(IsInvisibleGhost);
            if (mice.Count > 0)
            {
                return mice.OrderByDescending(mouse => mouse.GetComponent<MouseMover>().totalDistanceMoved).First();
            }
            return null;
        }
        
        private bool IsInvisibleGhost(GameObject mouse)
        {
            var ghostMouse = mouse.GetComponent<GhostMouse>();
            return ghostMouse != null && !ghostMouse.IsVisible();
        }
        
        private void HandleRangeBuff()
        {
            var buff = GetComponent<Buff>();
            if (buff != null)
            {
                SetRadius(RadiusWithoutBuff * buff.RangeMultiplier);

            }
        }

        private void ResizeRangeVisual()
        {
            rangeObject.transform.localScale = new Vector3(Radius * 2, Radius * 2, 1); 
        }

        protected override void SetRadius(float radius)
        { 
            ResizeRangeVisual();
            base.SetRadius(radius);
        }

        public void EnableRangeRenderer()
        {
            rangeSpriteRenderer.enabled = true;
        }
    }
}