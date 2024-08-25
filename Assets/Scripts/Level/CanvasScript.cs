using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    //Code credit to user PGJ1 from https://discussions.unity.com/t/stretch-canvas-to-fit-device/716883
    void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();

        Vector3 screenSize = Camera.main.ViewportToWorldPoint(Vector3.up + Vector3.right);

        screenSize *= 02;

        float sizeY = screenSize.y / rt.rect.height;
        float sizeX = screenSize.x / rt.rect.width;

        rt.localScale = new Vector3(sizeX, sizeY, 1);
    }
}
