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

    private int
        amountOfTypesOfMice =
            3; //this needs to be changed when more mice are added! MMA: 1- search MMA to find all of them

    private int[]
        amountOfEachMouse = { 0, 0, 0 }; // when more mice are added need to add more zeros to this as well MMA:2

    private float[] timeDelayOfEachMouse = { 0, 0, 0 }; //same with this one MMA:3
    private int miceSpawned;


    void Start()
    {
        LM = this;
        LoadLevel();
        StartCoroutine(wave());
    }


    /// <summary>fills the waves list with data from JSON file</summary>
    /// <remarks>Maintained by: Antosh</remarks>
    private void LoadLevel()
    {
        TextAsset jsonFile = Resources.Load("Waves/waves") as TextAsset;
        waves = JsonUtility.FromJson<Waves>(jsonFile.text);
    }

    /// <summary>Spawns a mouse of indicated mouse type</summary>
    /// <param>'mouseType' a number representing a mouse in the list in the same order as mouseTypesList</param>
    /// <remarks>Maintained by: Emily</remarks>
    private void spawnMouse(int mouseType)
    {
        GameObject newMouse =
            Instantiate(enemy, TurningPoints[0].position, transform.rotation); // Instantiate mouse prefab
        newMouse.GetComponent<MouseStats>().loadStats(mouseTypesList[mouseType]);
    }


    /// <summary>Puts the extra random mice into catogaries and sorts out amountOfEachMouse to be correct</summary>
    /// <remarks>Maintained by: Emily</remarks>
    private void chooseRandom(int amount)
    {
        for (int i = 0; i < amountOfTypesOfMice; i++)
        {
            //loads the correct values into relevant arrays
            amountOfEachMouse[i] = waves.waves[currentWave].mouseUnits[i].amount;
            timeDelayOfEachMouse[i] = waves.waves[currentWave].mouseUnits[i].timeOfWave;
        }

        for (int i = 0; i < amount; i++)
        {
            //this will have to be changed to depend on difficulty of random mice.
            int typeOfMouse = Random.Range(0, amountOfTypesOfMice);
            amountOfEachMouse[typeOfMouse]++;
        }
    }

    /// <summary>The process of one wave</summary>
    /// <remarks>Maintained by: Emily</remarks>
    IEnumerator wave()
    {
        miceSpawned = 0;
        float startTime = Time.time; //Time the wave begins
        int miceInWave = waves.waves[currentWave].totalMice; //number of mice that will be spawned in the wave
        bool[] started = { false, false, false }; //needs to be as long as the type of mice there are! MMA:4

        chooseRandom(waves.waves[currentWave].randomMouseUnits[0].amount);

        while (miceSpawned < miceInWave)
        {
            for (int mouseType = 0; mouseType < amountOfTypesOfMice; mouseType++) // for each mouse type
            {
                float timeNow = Time.time;
                if ((timeNow - startTime) > timeDelayOfEachMouse[mouseType] && started[mouseType] == false)
                {
                    StartCoroutine(spawnMouseUnit(amountOfEachMouse[mouseType], mouseType));
                    started[mouseType] = true;
                }

                yield return null;
            }
        }

        Debug.Log("All Mice have been Spawned Wave: " + currentWave);
    }

    /// <summary>Spawning one type of mouse, uses the Json to know the spaces between each mouse</summary>
    /// <remarks>Maintained by: Emily</remarks>
    IEnumerator spawnMouseUnit(int amountOfOneMouse, int whichMouse)
    {
        for (int i = 0; i < amountOfOneMouse; i++)
        {
            spawnMouse(whichMouse);
            miceSpawned++;
            yield return new WaitForSeconds(waves.waves[currentWave].mouseUnits[whichMouse].frequency);
        }
    }
}