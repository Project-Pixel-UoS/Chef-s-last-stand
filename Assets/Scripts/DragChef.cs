using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragChef : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject chef;
    [HideInInspector] public Transform parentAfterDrag;
    private Vector3 targetMapPos;
    private Vector3[] allPos;


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
    /// <param name = "targetMapPos"> is the position that mouse is pointing at.</param>
    /// <remarks>Maintained by: Lishan Xu</remarks>
    public void OnDrag(PointerEventData eventData)
    {
        MapManager MM = MapManager.MM;
        targetMapPos = MM.targetMapPos;
        allPos = MM.allPos;
        transform.position = Input.mousePosition;
    }

    /// <summary> Instantiate chef on the last overlapped map tile.</summary>
    /// <remarks>Maintained by: Lishan Xu</remarks>
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        if (targetMapPos!=null){
            Instantiate(chef, targetMapPos, transform.rotation);
        }
    }
}
