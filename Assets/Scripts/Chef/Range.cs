using System.Collections.Generic;
using Chef.Upgrades;
using DefaultNamespace;
using UnityEngine;

namespace Chef
{
    public class Range : MonoBehaviour
    {
        [SerializeField] private float radius; // range at which chef can attack mice
        public float Radius
        {
            get => radius;
            set => SetRadius(value);
        }
        
        
        [SerializeField] private GameObject rangeObject; //imports the chefs range.
        private SpriteRenderer rangeSpriteRenderer;
        private bool clicked = false; //Used to see if you are clicking on or off the chef


        void Start()
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
            var buffedRadius = GetBuffedRadius();
            var mice = GameObject.FindGameObjectsWithTag("Mouse");
            var miceInRange = new List<GameObject>();
            foreach (var mouse in mice)
            {
                float distance = (mouse.transform.position - transform.position).magnitude;
                
                if (distance <= buffedRadius)
                {
                    miceInRange.Add(mouse);
                }
            }

            return miceInRange;
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
            if (!clicked)
            {
                // when chef is pressed while unselected
                rangeSpriteRenderer.enabled = true;
                clicked = true;
                ChefTracker.Instance.CurrentChef = gameObject;
            }
            else
            {
                // when chef is pressed while selected
                rangeSpriteRenderer.enabled = false;
                clicked = false;
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
                    if (clicked && !Utils.checkMousePosOutsideMap())
                    {
                        rangeSpriteRenderer.enabled = false;
                        clicked = false;
                        ChefTracker.Instance.CurrentChef = null;

                    }
                }
            }
        }
        
        private void ResizeRangeVisual()
        {
            rangeObject.transform.localScale =
                new Vector3(radius * 2, radius * 2, 1); //makes the range the same size as chosen
        }

        private void SetRadius(float radius)
        {
            ResizeRangeVisual();
            this.radius = radius;
            // ResizeRangeVisual();
        }
    }
}