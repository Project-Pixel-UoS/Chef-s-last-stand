using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager LM;
    public Transform[] TurningPoints;
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