using System.Collections;
using System.Collections.Generic;
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
        // Debug.Log("Begin drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    /// <summary> Make the item follow the mouse.</summary>
    /// <remarks>Maintained by: Lishan Xu</remarks>
    public void OnDrag(PointerEventData eventData)
    { 
        transform.position = Input.mousePosition;

        // Convert mouse position to viewport coordinates
        Vector3 viewportPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        Debug.Log(viewportPos);

        // Ensure the position remains within the camera's viewport
        viewportPos.x = Mathf.Clamp01(viewportPos.x);
        viewportPos.y = Mathf.Clamp01(viewportPos.y);
        viewportPos.z = Mathf.Clamp(viewportPos.z, 0, mainCamera.farClipPlane);

        // Convert back from viewport to world space
        dropPosition = mainCamera.ViewportToWorldPoint(viewportPos);
        dropPosition.z = 0;
    }

    /// <summary> Instantiate chef on the last overlapped map tile.</summary>
    /// <remarks>Maintained by: Lishan Xu</remarks>
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        // dont allow player to place a chef on game over screen, or if has too little credits
        if(!healthManager.IsGameOver() && creditsManager.SpendCredits(chefCost))
        {
            Instantiate(chef, dropPosition, transform.rotation);
        }
        
    }
}
