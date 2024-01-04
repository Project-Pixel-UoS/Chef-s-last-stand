using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMap : MonoBehaviour
{
    private bool overlap = false;
    void OnMouseOver()
    {
        // if (!Input.GetMouseButtonUp(0)){
        //     Debug.Log("Mouse is over GameObject.");
        // }
        overlap = true;
    }

    void OnMouseExit()
    {
        overlap = false;
    }

    public bool IsOverlapping(){
        return overlap;
    }
}
