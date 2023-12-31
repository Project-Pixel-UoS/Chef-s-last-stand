using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager LM;
    // public Transform startPoint;
    // public Transform endPoint;
    public Transform[] TurningPoints;
    
    public float moveSpeed = 10f;

    public GameObject enermy;

    void Start()
    {
        LM = this;
        InvokeRepeating("spawnMouse", 0, 5);
    }

    private void spawnMouse()
    {
        Instantiate(enermy, TurningPoints[0].position, transform.rotation);
    }
}
