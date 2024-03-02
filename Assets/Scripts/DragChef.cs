using System.Collections;
using System.Collections.Generic;
using GameManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragChef : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject chef;
    public Camera mainCamera;
    [HideInInspector] public Transform parentAfterDrag;
    private Vector3 dropPosition;
    [SerializeField] private int chefCost;
    private GameObject credits;
    private CreditManager creditsManager;
    private Image image;
    private Image slot;

    private void Start()
    {
        credits = GameObject.FindGameObjectWithTag("Credits");
        creditsManager = credits.GetComponent<CreditManager>();
        image = GetComponent<Image>();
        slot = transform.parent.GetComponent<Image>();
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

        transform.position = Input.mousePosition;

        // Convert mouse position to viewport coordinates
        Vector3 viewportPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        Debug.Log(viewportPos);

        // Ensure the position remains within the camera's viewport
        viewportPos.x = Mathf.Clamp(viewportPos.x, 0, 1);
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0, 1);
        viewportPos.z = Mathf.Clamp(viewportPos.z, 0, mainCamera.farClipPlane);

        if (CheckOutOfBounds(viewportPos))
        {
            Debug.Log("OUT OF BOUNDS");
        }

        // Convert back from viewport to world space
        dropPosition = mainCamera.ViewportToWorldPoint(viewportPos);
        dropPosition.z = 0;
    }


    private bool CheckOutOfBounds(Vector3 viewportPos)
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
            Instantiate(chef, dropPosition, transform.rotation);
        }
    }
}