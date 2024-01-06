using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager LM;
    public Transform[] TurningPoints;
    public GameObject enemy;
    public MiceScriptableObject[] mouseTypesList;       // List of MiceScriptableObjects, one for each type of mouse, containing their stats

    void Start()
    {
        LM = this;
        InvokeRepeating("spawnMouse", 0, 5);
    }

    private void spawnMouse()
    {
        GameObject newMouse = Instantiate(enemy, TurningPoints[0].position, transform.rotation);        // Instantiate mouse prefab
        newMouse.GetComponent<SpriteMove>().loadStats(mouseTypesList[Random.Range(0, 3)]);      // Set mouse's type using ScriptableObject (currently random)
    }
}