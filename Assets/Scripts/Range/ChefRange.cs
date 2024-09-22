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
        
        private void HandleRangeBuff()//TODO this needs an update because it onnly makes the circles bigger not the acutal range
        {
            Buff bf = GetComponent<Buff>();
            if (bf != null)
            {
                rangeObject.transform.localScale =
                    new Vector3(radius * 2 * bf.RangeMultiplier, radius * 2 * bf.RangeMultiplier,
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
            Debug.Log("visual radius "+radius);
            ResizeRangeVisual();
            base.SetRadius(radius);
        }

        public void EnableRangeRenderer()
        {
            rangeSpriteRenderer.enabled = true;
        }
    }
}