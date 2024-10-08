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