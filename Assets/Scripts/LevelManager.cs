using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager LM;
    public Transform[] TurningPoints;
    public GameObject enemy;


    void Start()
    {
        LM = this;
        InvokeRepeating("spawnMouse", 0, 5);
    }

    private void spawnMouse()
    {
        Instantiate(enemy, TurningPoints[0].position, transform.rotation);
    }
}