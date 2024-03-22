using UnityEngine;

namespace Chef
{
    public class Range : MonoBehaviour
    {
        public float radius; // range at which chef can attack mice
        [SerializeField] private GameObject rangeObject; //imports the chefs range.
        private SpriteRenderer rangeSpriteRenderer;
        private bool clicked = false; //Used to see if you are clicking on or off the chef


        void Start()
        {
            // print("RANGE OBJET SCALE " + rangeObject.transform.localScale);
            rangeSpriteRenderer = rangeObject.GetComponent<SpriteRenderer>();
            rangeObject.transform.localScale =
                new Vector3(radius * 2, radius * 2, 1); //makes the range the same size as chosen
            rangeSpriteRenderer.enabled = false;
        }

        void Update()
        {
            ClickManager();
        }

        /// <summary> Activates when the Chef is pressed </summary>
        /// <remarks>Maintained by: Emily Johnston </remarks>
        void OnMouseDown()
        {
            if (!clicked)
            {
                rangeSpriteRenderer.enabled = true;
                clicked = true;
            }
            else
            {
                rangeSpriteRenderer.enabled = false;
                clicked = false;
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
                    if (clicked)
                    {
                        rangeSpriteRenderer.enabled = false;
                        clicked = false;
                    }
                }
            }
        }
    }
}