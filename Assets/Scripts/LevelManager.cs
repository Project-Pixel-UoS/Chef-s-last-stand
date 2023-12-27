using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager LM;
    // public Transform startPoint;
    // public Transform endPoint;
    public Transform[] TurningPoints;
    
    public float moveSpeed = 2f;

    public GameObject enermy;

    void Start()
    {
        LM = this;
        if (enermy != null)
        {
            enermy.AddComponent<SpriteMove>(); 
        }
        Instantiate(enermy, TurningPoints[0].position, transform.rotation);
    }
}
