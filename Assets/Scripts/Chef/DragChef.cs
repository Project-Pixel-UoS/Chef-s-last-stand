using System.Collections;
using System.Collections.Generic;
using GameManagement;
using Chef;
using GameManagement;
using Shop;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DragChef : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject chef;
    public Image range; //range that appears when chef is dragged

    private Collider2D chefCollider2D;
    [HideInInspector] public Transform parentAfterDrag;
    private Vector3 dropPosition;

    private ShopSlotManager shopSlotManager;

    [SerializeField] private GameObject placeableAreas;

    private void Start()
    {

        shopSlotManager = GetComponent<ShopSlotManager>();
        float rangeRadius = chef.GetComponent<Range>().Radius; //get the radius size from chef prefab
        range.enabled = false; //hides the range at the beginning

        Vector3 rangeSize = (Camera.main.WorldToScreenPoint(new Vector3(rangeRadius, rangeRadius, 0))
                             - Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0))) * 2;
        range.rectTransform.sizeDelta = new Vector2(rangeSize.x, rangeSize.y);

        // image = GetComponent<Image>();
        // slot = transform.parent.GetComponent<Image>();

        chefCollider2D = GetComponent<Collider2D>();
    }


    /// <summary> Pin the item while dragging.</summary>
    /// <remarks>Maintained by: Lishan Xu</remarks>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GameManager.isPaused)
        {
            return;
        }

        range.enabled = true; // makes the range visible

        parentAfterDrag = transform.parent;
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

        PositionChefOntoCursor();
        if (CheckOutOfBounds())
        {
            range.color = new Color32(238, 68, 56, 135);
        }
        else
        {
            range.color = new Color32(0, 220, 255, 135);
        }

        // Convert back from viewport to world space
        dropPosition = transform.position;
        dropPosition.z = 0;
    }

    private void PositionChefOntoCursor()
    {
        // canvas is in world screen mode so we need to convert to world units
        Vector3 worldCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldCursorPos.z = 0;
        transform.position = worldCursorPos;
    }


    private bool CheckOutOfBounds()
    {
        return CheckIntersectingExistingChef() || CheckInArea();
    }

    /// <summary>
    /// Checks if the chef that is being dragged is touch a chef that is already placed
    /// </summary>
    /// <returns>true if chefs are intersecting</returns>
    /// <remarks>Maintainer: Antosh</remarks>
    private bool CheckIntersectingExistingChef()
    {
        foreach (var _collider2D in GetAllChefColliders())
        {
            if (chefCollider2D.bounds.Intersects(_collider2D.bounds))
            {
                return true;
            }
        }

        return false;
    }


    /// <returns>A list of all the colliders of the chefs that are already placed</returns>
    /// <remarks>Maintainer: Ying and Antosh</remarks>
    private List<Collider2D> GetAllChefColliders()
    {
        var colliders = new List<Collider2D>();
        foreach (var chef in GetAllChefs())
        {
            colliders.Add(chef.GetComponent<Collider2D>());
        }

        return colliders;
    }

    /// <returns>A list of all the chefs that are already placed</returns>
    /// <remarks>Maintainer: Ying and Antosh</remarks>
    private List<GameObject> GetAllChefs()
    {
        var chefParent = GameObject.FindGameObjectWithTag("ChefContainer");
        var chefs = new List<GameObject>();
        //iterate over each child transform
        foreach (Transform _transform in chefParent.transform)
        {
            chefs.Add(_transform.gameObject);
        }

        return chefs;
    }
    
    /// <returns> True if chef is on the mice's path </returns>
    private bool CheckOnPath()
    {
        bool top = Physics.Raycast(transform.position, transform.up);
        bool bottom = Physics.Raycast(transform.position, -transform.up);
        bool left = Physics.Raycast(transform.position, transform.right);
        bool right = Physics.Raycast(transform.position, -transform.right);
        bool above = Physics.Raycast(new Vector3(transform.position.x, transform.position.y, -5), transform.forward);

        return (bottom && top) || (left && right) || above;
    }

    /// <summary>
    /// Checks if chef is in a placeable area
    /// </summary>
    /// <returns>true if chefs is in placeable area</returns>
    /// <remarks>Maintainer: Ben Brixton</remarks>
    private bool CheckInArea()
    {
        foreach (Transform child in placeableAreas.transform){
            if(chefCollider2D.bounds.Intersects(child.gameObject.GetComponent<BoxCollider2D>().bounds)){
                return false;
            }
        }
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");
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
        range.enabled = false;

        // dont allow player to place a chef on game over screen, or if has too little credit
        bool sufficientFunds = shopSlotManager.HandleCreditTransaction();
        if (!GameManager.gameManager.IsGameOver() && sufficientFunds && !CheckOutOfBounds())
        {
            var chefParent = GameObject.FindGameObjectWithTag("ChefContainer");
            Instantiate(chef, dropPosition, chef.transform.rotation, chefParent.transform);
        }
    }
}