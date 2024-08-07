using System.Collections.Generic;
using Chef.Upgrades;
using UnityEngine;
using Util;

namespace Chef
{
    public class Range : MonoBehaviour
    {
        [SerializeField] private float radius; // range at which chef can attack mice
        [SerializeField] private GameObject rangeObject; //imports the chefs range.
        private SpriteRenderer rangeSpriteRenderer;

        public float Radius
        {
            get => radius;
            set => SetRadius(value);
        }

        void Awake()
        {
            rangeSpriteRenderer = rangeObject.GetComponent<SpriteRenderer>();
            ResizeRangeVisual();
            rangeSpriteRenderer.enabled = false;
        }

        void Update()
        {
            ClickManager();
            HandleRangeBuff();
        }
        

        /// <returns>
        /// mice in range of the chef
        /// </returns>
        /// <remarks> maintained by: Antosh </remarks>
        public List<GameObject> GetMiceInRange()
        {
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

        public bool IsMouseInRange(GameObject mouse)
        {
            return GetMiceInRange().Contains(mouse);
        }

        private float GetBuffedRadius()
        {
            Buff bf = GetComponent<Buff>();

            float buffedRadius = radius;
            if (bf != null)
            {
                buffedRadius *= bf.rangeIncrease;
            }

            return buffedRadius;
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

        /// <summary> Activates when the Chef is pressed </summary>
        /// <remarks>Maintained by: Emily Johnston </remarks>
        void OnMouseDown()
        {
            if (ChefTracker.Instance.CurrentChef != gameObject)
            { 
                // when chef is pressed while unselected
                //disable previous chefs range
                if (ChefTracker.Instance.CurrentChef != null)
                {
                    var rangeComp = ChefTracker.Instance.CurrentChef.GetComponent<Range>();
                    rangeComp.rangeSpriteRenderer.enabled = false;
                }
               
                rangeSpriteRenderer.enabled = true;
                ChefTracker.Instance.CurrentChef = gameObject;
            }
            else
            {
                // when chef is pressed while selected
                rangeSpriteRenderer.enabled = false;
                ChefTracker.Instance.CurrentChef = null;

            }
        }

   
        
        

        /// <summary> Checks what is being clicked </summary>
        /// <remarks>Maintained by: Emily Johnston </remarks>
        public void ClickManager()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
             
                if (hit.collider == null)
                {
                    if (ChefTracker.Instance.CurrentChef == gameObject && !Utils.checkMousePosOutsideMap())
                    {
                        rangeSpriteRenderer.enabled = false;
                        ChefTracker.Instance.CurrentChef = null;

                    }
                }
            }
        }
        
        public void ResizeRangeVisual()
        {
            rangeObject.transform.localScale =
                new Vector3(radius * 2, radius * 2, 1); //makes the range the same size as chosen
        }

        private void SetRadius(float radius)
        {
            ResizeRangeVisual();
            this.radius = radius;
        }

        public void EnableRangeRenderer()
        {
            rangeSpriteRenderer.enabled = true;
        }
    }
}