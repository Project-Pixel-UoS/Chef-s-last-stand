using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager LM;
    public Transform[] TurningPoints;

    public GameObject enermy;
    public Image range;


    void Start()
    {
        LM = this;
        InvokeRepeating("spawnMouse", 0, 5);
        range.enabled=false;//makes the range of the chef in the slot invisible
    }

    private void spawnMouse()
    {
        Instantiate(enermy, TurningPoints[0].position, transform.rotation);
    }
}