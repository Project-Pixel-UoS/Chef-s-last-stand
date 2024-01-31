using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager LM;
    public Transform[] TurningPoints;
    public GameObject enemy;
    public MiceScriptableObject[] mouseTypesList;

    private List<Wave> waves = new List<>();

    void Start()
    {
        LM = this;
        LoadLevel();
        // InvokeRepeating("spawnMouse", 0, 5);
    }

    /// <summary>fills the waves list with data from JSON file</summary>
    /// <remarks>Maintained by: Antosh</remarks>
    private void LoadLevel()
    {

    }

    private void spawnMouse()
    {
        GameObject newMouse = Instantiate(enemy, TurningPoints[0].position, transform.rotation);        // Instantiate mouse prefab
        newMouse.GetComponent<MouseStats>().loadStats(mouseTypesList[Random.Range(0, 4)]);
    }

    
}