using UnityEngine;
namespace Range
{
    public class ChefRange : Range
    {
        [SerializeField] private GameObject rangeObject; //imports the chefs range.
        private SpriteRenderer rangeSpriteRenderer;
        

        void Awake()
        {
            rangeSpriteRenderer = rangeObject.GetComponent<SpriteRenderer>();
            ResizeRangeVisual();
            rangeSpriteRenderer.color = Color.Color.rangeColor;
            rangeSpriteRenderer.enabled = false;

        }

        void Update()
        {
            HandleRangeBuff();
        }
        
        private void HandleRangeBuff()
        {
            Buff bf = GetComponent<Buff>();
            if (bf != null)
            {
                rangeObject.transform.localScale =
                    new Vector3(radius * 2 * bf.rangeIncrease, radius * 2 * bf.rangeIncrease,
                        1); //makes the range the same size as chosen
            }
            else
            {
                rangeObject.transform.localScale =
                    new Vector3(radius * 2, radius * 2, 1); //makes the range the same size as chosen
            }
        }

        private void ResizeRangeVisual()
        {
            rangeObject.transform.localScale =
                new Vector3(radius * 2, radius * 2, 1); //makes the range the same size as chosen
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