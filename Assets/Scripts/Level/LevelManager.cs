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
    private Waves waves;

    private int currentWave = 0;
    private int amountOfTypesOfMice=3;//this needs to be changed when more mice are added!! Remeber to minus one



    void Start()
    {
        LM = this;
        LoadLevel();
        //InvokeRepeating("spawnMouse", 0, 5);
        int[] amountOfEachMouse={0,0,0,0};// when more mice are added need to add more zeros to this as well
        wave(amountOfEachMouse,currentWave);
    }

    /// <summary>fills the waves list with data from JSON file</summary>
    /// <remarks>Maintained by: Antosh</remarks>
    private void LoadLevel()
    {
        TextAsset jsonFile = Resources.Load("Waves/waves") as TextAsset;
        waves = JsonUtility.FromJson<Waves>(jsonFile.text);
        Debug.Log(waves.waves[1].randomMouseUnits[1].amount);
    }

    private void spawnMouse()
    {
        
        GameObject newMouse =
            Instantiate(enemy, TurningPoints[0].position, transform.rotation); // Instantiate mouse prefab
        newMouse.GetComponent<MouseStats>().loadStats(mouseTypesList[Random.Range(0, 4)]);
    }

    
    /// <summary>Puts the extra mice in random catogaries</summary>
    /// <remarks>Maintained by: Emily</remarks>

    private void chooseRandom(int amount,int[] amountOfEachMouse)
    {
        int typeOfMouse;
        for(int i=0;i<amount;i++ ){
            typeOfMouse=Random.Range(0,amountOfTypesOfMice);
            amountOfEachMouse[typeOfMouse]++;
        }

    }

     /// <summary>The process of one wave</summary>
    /// <remarks>Maintained by: Emily</remarks>

    private void wave(int[] amountOfEachMouse,int currentWave)
    {
        chooseRandom(waves.waves[currentWave].randomMouseUnits[0].amount,amountOfEachMouse);


        Debug.Log(amountOfEachMouse[0]);
        Debug.Log(amountOfEachMouse[1]);
        Debug.Log(amountOfEachMouse[2]);
        Debug.Log(amountOfEachMouse[3]);
    }
}