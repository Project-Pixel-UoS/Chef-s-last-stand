using System.Collections;
using System.Collections.Generic;
using Chef;
using GameManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DragChef : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject chef;
    [SerializeField] private GameObject chefParent;//empty parent object that contains all the chefs
    private BoxCollider2D chefCollider2D;
    
    public Camera mainCamera;
    public Image range; //range that appears when chef is dragged
    [HideInInspector] public Transform parentAfterDrag;
    private Vector3 dropPosition;
    [SerializeField] private int chefCost;

    private RectTransform rectTransform;

    private CreditManager creditsManager;
    private Image image;
    private Image slot;

    private void Start()
    {
        
        GameObject credits = GameObject.FindGameObjectWithTag("Credits");
        creditsManager = credits.GetComponent<CreditManager>();
        
        rectTransform = GetComponent<RectTransform>();
        float rangeNumber = chef.GetComponent<Range>().radius;
        range.enabled = false; //hides the range at the beginning

        Vector3 rangeSize = (Camera.main.WorldToScreenPoint(new Vector3(rangeNumber, rangeNumber, 0)) 
                            - Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0))) * 2;
        range.rectTransform.sizeDelta = new Vector2(rangeSize.x, rangeSize.y);
        
        image = GetComponent<Image>();
        slot = transform.parent.GetComponent<Image>();

        chefCollider2D = GetComponent<BoxCollider2D>();


    }

    private void Update()
    {
        if (chefCost > creditsManager.GetCredits())
        {
            image.color = Color.red;
            slot.color = Color.red;
        }
        else
        {
            image.color = Color.white;
            slot.color = new Color(0.86f, 0.61f, 0.21f);
        }
    }

    [SerializeField] private HealthManager healthManager;


    /// <summary> Pin the item while dragging.</summary>
    /// <remarks>Maintained by: Lishan Xu</remarks>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GameManager.isPaused)
        {
            return;
        }

        // Debug.Log("Begin drag");

        range.enabled = true; // makes the range visible
        parentAfterDrag = transform.parent;//
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    /// <summary> Make the item follow the mouse.</summary>
    /// <remarks>Maintained by: Lishan Xu</remarks>
    public void OnDrag(PointerEventData eventData)
    {
        if (GameManager.isPaused)
        {
            return;
        }

        // canvas is in world screen mode so we need to convert to world units
        transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0); //z must be 0 to be visible

        // Convert mouse position to viewport coordinates
        Vector3 viewportPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);

        // Ensure the position remains within the camera's viewport
        viewportPos.x = Mathf.Clamp(viewportPos.x, 0, 1);
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0, 1);
        viewportPos.z = Mathf.Clamp(viewportPos.z, 0, mainCamera.farClipPlane);

        if (CheckOutOfBounds(viewportPos))
        {
            range.color = new Color32(238, 68, 56, 135);
        }
        else
        {
            range.color = new Color32(0, 220, 255, 135);
        }

        // Convert back from viewport to world space
        dropPosition = mainCamera.ViewportToWorldPoint(viewportPos);
        dropPosition.z = 0;
    }


    private bool CheckOutOfBounds(Vector3 viewportPos)
    {

        // float canvasWidth = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().scaleFactor;
        // Vector2 worldPos = Camera.main.ViewportToWorldPoint(viewportPos);
        // var sizeDelta = rectTransform.sizeDelta / canvasWidth;
        // Vector2 topRight = worldPos + sizeDelta / 2;
        // Vector2 topLeft = worldPos + new Vector2(-1, 1) * sizeDelta / 2;
        // Vector2 bottomLeft = worldPos + new Vector2(-1, -1) * sizeDelta / 2;
        // Vector2 bottomRight = worldPos + new Vector2(1, -1) * sizeDelta / 2;
        
        // Debug.Log("SIZE DELTA: " + sizeDelta);
        // print("TOP LEFT: " + topLeft);
        // print("TOP RIGHT: " + topRight);
        // print("BOTTOM LEFT: " + bottomLeft);
        // print("BOTTOM RIGHT: " + bottomRight);
        // print("POS: "+ worldPos);
        // print("----------------------------------------");


        

        print("chef collider 2d x: " + chefCollider2D.bounds.center.x);
        print("chef collider 2d y: " + chefCollider2D.bounds.center.y);

        foreach (var _collider2D in GetAllChefColliders())
        {
            
            Debug.Log("placed chef collider: " + chefCollider2D.bounds.Intersects(_collider2D.bounds));
            if (chefCollider2D.bounds.Intersects(_collider2D.bounds))
            {
                return true;
            }
            // if (collider2D.bounds.Contains(topRight))
            // {
            //     return true;
            // }
            //
            // if (collider2D.bounds.Contains(topLeft))
            // {
            //     return true;
            // }
            //
            // if (collider2D.bounds.Contains(bottomLeft))
            // {
            //     return true;
            // }
            //
            // if (collider2D.bounds.Contains(bottomRight))
            // {
            //     return true;
            // }
        }

        return CheckOutsideScreen(viewportPos);
    }

    private List<BoxCollider2D> GetAllChefColliders()
    {
        var colliders = new List<BoxCollider2D>();
        foreach (var chef in GetAllChefs())
        {
            colliders.Add(chef.GetComponent<BoxCollider2D>());
        }

        return colliders;
    }

    private List<GameObject> GetAllChefs()
    {
        var chefs = new List<GameObject>();
        //iterate over each child transform
        foreach (Transform _transform in chefParent.transform)
        {
            chefs.Add(_transform.gameObject);
        }

        return chefs;
    }

    /// <summary>
    /// Returns true if 
    /// </summary>
    /// <param name="viewportPos"></param>
    /// <returns></returns>
    private static bool CheckOutsideScreen(Vector3 viewportPos)
    {
        if (viewportPos.x <= 0.02 || viewportPos.x >= 0.85)
        {
            return true;
        }

        if (viewportPos.y <= 0.05 || viewportPos.y >= 0.95)
        {
            return true;
        }

        return false;
    }


    /// <summary> Instantiate chef on the last overlapped map tile.</summary>
    /// <remarks>Maintained by: Lishan Xu</remarks>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (GameManager.isPaused)
        {
            return;
        }

        transform.SetParent(parentAfterDrag);
        
        // dont allow player to place a chef on game over screen, or if has too little credits
        if (!GameManager.gameManager.IsGameOver() && creditsManager.SpendCredits(chefCost) &&
            !CheckOutOfBounds(mainCamera.ScreenToViewportPoint(Input.mousePosition)))
        {
            Instantiate(chef, dropPosition, transform.rotation, chefParent.transform);
        }

        range.enabled = false;
    }
}