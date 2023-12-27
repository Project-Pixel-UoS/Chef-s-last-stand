
using System;
using UnityEngine;

public class Chef : MonoBehaviour
{

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


    /// <returns> mouse that is furthest along route, and in range of the chef </returns>
    /// <remarks>Maintained by: Antosh </remarks>
    /// <todo> Verify that the mouse is in range of the current chef </todo>
    private GameObject GetFurthestMouseInRange()
    {
        return GameObject.FindGameObjectWithTag("Mouse");
    }
}
