using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager LM;
    public Transform[] TurningPoints;
    public GameObject enermy;
    public int credits;

    void Start()
    {
        LM = this;
        InvokeRepeating("spawnMouse", 0, 5);
        //some arbitrary number for now.
        credits = 50;
    }

    private void spawnMouse()
    {
        Instantiate(enermy, TurningPoints[0].position, transform.rotation);
    }

    public void increaseMoney(int amount)
    {
        credits += amount;
    }

    public bool spendCredits(int amount)
    {
        if (credits >= amount)
        {
            credits -= amount;
            return true;
        }
        else
        {
            //console log for now.
            Debug.Log("no money");
            return false;
        }
    }
}