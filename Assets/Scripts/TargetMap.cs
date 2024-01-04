using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMap : MonoBehaviour
{
    private bool overlap = false;
    /// <summary> Check whether the mouse is over this map tile.</summary>
    /// <param name = "overlap"> detect whether the mouse is overlapped the map tile</param>
    /// <remarks>Maintained by: Lishan Xu</remarks>
    void OnMouseOver()
    {
        // if (!Input.GetMouseButtonUp(0)){
        //     Debug.Log("Mouse is over GameObject.");
        // }
        overlap = true;
    }

    /// <summary> Check whether the mouse is not over this map tile.</summary>
    /// <remarks>Maintained by: Lishan Xu</remarks>
    void OnMouseExit()
    {
        overlap = false;
    }

    public bool IsOverlapping(){
        return overlap;
    }
}
