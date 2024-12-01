using System;
using System.Collections;
using System.Collections.Generic;
using GameManagement;
using Chef;
using Chef.Upgrades;
using Music;
using Range;
using Shop;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Util;


public class DragChef : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject chef;
    private SpriteRenderer range; //range that appears when chef is dragged

    private Collider2D chefCollider2D;
    [HideInInspector] public Transform parentAfterDrag;
    private Vector3 dropPosition;

    private ChefForSale chefForSale;
    [SerializeField] private GameObject placeableAreas;

    private RectTransform rectTransform;

    private void Start()
    {
        chefForSale = GetComponent<ChefForSale>();
        float rangeRadius = chef.GetComponent<ChefRange>().Radius; //get the radius size from chef prefab
        rectTransform = GetComponent<RectTransform>();
        DisplayChefPrice();
        SetupRange(rangeRadius);
        chefCollider2D = GetComponent<Collider2D>();
    }

    private void SetupRange(float rangeRadius)
    {
        range = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Vector3 rangeSize = (Camera.main.WorldToScreenPoint(new Vector3(rangeRadius, rangeRadius, 0))
                             - Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0))) * 2;
        range.enabled = false; //hides the range at the beginning
        range.transform.localScale = rangeSize;
        Utils.ResizeSpriteInsideCanvas(range.gameObject);
    }

    private void DisplayChefPrice()
    {
        gameObject.transform.parent.GetComponentInChildren<Text>().text = chefForSale.cost.ToString() + '$';
    }


    /// <summary> Pin the item while dragging.</summary>
    /// <remarks>Maintained by: Lishan Xu</remarks>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GameManager.isPaused) return;
        if (!chefForSale.CheckSufficientChefFunds())
        {
            Utils.PlayInvalidTransactionSound(gameObject);
            return;
        }

        range.enabled = true; // makes the range visible

        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        ChefTracker.Instance.CurrentChef = null;
    }

    /// <summary> Make the item follow the mouse.</summary>
    /// <remarks>Maintained by: Lishan Xu</remarks>
    public void OnDrag(PointerEventData eventData)
    {
        if (GameManager.isPaused) return;
        if (!chefForSale.CheckSufficientChefFunds())
        {
            Utils.PlayInvalidTransactionSound(gameObject);
            return;
        }
        
        PositionChefOntoCursor();
        SetRangeColour();
        SetDropPosition();
    }

    private void SetDropPosition()
    {
        // Convert back from viewport to world space
        dropPosition = transform.position;
        dropPosition.z = 0;
    }

    private void SetRangeColour()
    {
        if (CheckOutOfBounds())
        {
            range.color = new Color32(238, 68, 56, 135);
        }
        else
        {
            range.color = Color.Color.rangeColor;
        }
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
    private List<BoxCollider2D> GetAllChefColliders()
    {
        var colliders = new List<BoxCollider2D>();
        foreach (var chef in GetAllChefs())
        {
            colliders.Add(chef.GetComponent<BoxCollider2D>());
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

    /// <summary>
    /// Checks if chef is in a placeable area
    /// </summary>
    /// <returns>true if chefs is in placeable area</returns>
    /// <remarks>Maintainer: Ben Brixton</remarks>
    private bool CheckInArea()
    {
        foreach (Transform child in placeableAreas.transform)
        {
            if (chefCollider2D.bounds.Intersects(child.gameObject.GetComponent<Collider2D>().bounds))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary> Instantiate chef on the last overlapped map tile.</summary>
    /// <remarks>Maintained by: Lishan Xu</remarks>
    public void OnEndDrag(PointerEventData eventData)
    {

        if (GameManager.isPaused) return;
        if (GameManager.gameManager.IsGameOver())return;
        if (!chefForSale.CheckSufficientChefFunds())
        {
            Utils.PlayInvalidTransactionSound(gameObject);
            return;
        }
        
        ReturnSpriteToSlot();
        range.enabled = false;

        if (CheckOutOfBounds())
        {
            Utils.PlayInvalidTransactionSound(gameObject);
            return;
        }

        chefForSale.HandleChefTransaction();
        var chefParent = GameObject.FindGameObjectWithTag("ChefContainer");
        var chefInstance = Instantiate(chef, dropPosition, chef.transform.rotation, chefParent.transform);
        Utils.ResizeSpriteOutsideCanvas(chefInstance);
        SoundPlayer.instance.PlayPurchaseFX();
    }
    
    private void ReturnSpriteToSlot()
    {
        transform.SetParent(parentAfterDrag);
        rectTransform.localPosition = new Vector2(0, 0);
    }
}