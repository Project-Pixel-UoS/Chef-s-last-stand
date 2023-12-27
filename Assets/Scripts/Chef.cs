
using System;
using UnityEngine;

public class Chef : MonoBehaviour
{

    [SerializeField] private float range;

    void Update()
    {
        GameObject furthestMouse = GetFurthestMouseInRange();
        if (furthestMouse == null) return;
        Rotate(furthestMouse);
    }
    

    /// <summary> Spins chef so that he is facing the mouse </summary>
    /// <param name = "furthestMouse"> mouse which chef will point towards</param>
    /// <remarks>Maintained by: Antosh Nikolak</remarks>
    private void Rotate(GameObject furthestMouse)
    {
        Vector3 direction = transform.position - furthestMouse.transform.position;
        float radians = Mathf.Atan2(direction.x, direction.y);
        float degrees = radians * Mathf.Rad2Deg;
        Quaternion target = Quaternion.Euler(0, 0, degrees);
        transform.rotation = target;
    }



    /// <returns> find an arbitrary mouse that is in range </returns>
    /// <remarks>Maintained by: Antosh </remarks>
    /// <todo> get the mouse that is furthest alon </todo>
    private GameObject GetFurthestMouseInRange()
    {
        return GameObject.FindGameObjectWithTag("Mouse");
        // var mice = GameObject.FindGameObjectsWithTag("Mouse");
        // foreach (var mouse in mice)
        // {
        //     float distance = (mouse.transform.position - transform.position).magnitude;
        //     if (distance <= range)
        //     {
        //         return mouse;
        //     }
        // }
        //
        // return null;
    }

}
