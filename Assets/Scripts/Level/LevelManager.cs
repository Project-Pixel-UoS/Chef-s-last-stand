using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class LevelManager : MonoBehaviour
{
    public static LevelManager LM;
    public Transform[] TurningPoints;
    public GameObject enemy;
    public MiceScriptableObject[] mouseTypesList;
    private Waves waves;
    

    private int currentWave = 0;
    void Start()
    {
        LM = this;
        LoadLevel();
        StartWave();
    }


    /// <summary>fills the waves list with data from JSON file</summary>
    /// <remarks>Maintained by: Antosh</remarks>
    private void LoadLevel()
    {
        TextAsset jsonFile = Resources.Load("Waves/waves") as TextAsset;
        waves = JsonUtility.FromJson<Waves>(jsonFile.text);
    }

    /// <summary>Spawns a mouse of indicated mouse type</summary>
    /// <param name="mouseType"> type of mouse that will be spawned in</param>
    /// <remarks>Maintained by: Emily</remarks>
    private void SpawnMouse(MiceScriptableObject mouseType)
    {
        GameObject newMouse =
            Instantiate(enemy, TurningPoints[0].position, transform.rotation); // Instantiate mouse prefab
        newMouse.GetComponent<MouseStats>().loadStats(mouseType);
    }

    private MiceScriptableObject GetMouseType(string mouseName)
    {
        foreach (var mouseType in mouseTypesList)
        {
            if (mouseType.mouseName == mouseName)
            {
                return mouseType;
            }
        }
        throw new ArgumentException("The mouse that your provided doesnt exist!");
    }


    /// <summary>Puts the extra random mice into catogaries and sorts out amountOfEachMouse to be correct</summary>
    /// <remarks>Maintained by: Emily</remarks>
    private void chooseRandom(int amount)
    {
        // for (int i = 0; i < amountOfTypesOfMice; i++)
        // {
        //     //loads the correct values into relevant arrays
        //     amountOfEachMouse[i] = waves.waves[currentWave].mouseUnits[i].amount;
        //     timeDelayOfEachMouse[i] = waves.waves[currentWave].mouseUnits[i].timeOfWave;
        // }

        // for (int i = 0; i < amount; i++)
        // {
        //     //this will have to be changed to depend on difficulty of random mice.
        //     int typeOfMouse = Random.Range(0, amountOfTypesOfMice);
        //     amountOfEachMouse[typeOfMouse]++;
        // }
    }

    /// <summary>The process of one wave</summary>
    /// <remarks>Maintained by: Emily</remarks>
    void StartWave()
    {
        foreach (var mouseUnit in waves.waves[currentWave].mouseUnits)
        {
            StartCoroutine(SpawnMouseUnitWithDelay(mouseUnit));
        }
        
        foreach (var randomMouseUnit in waves.waves[currentWave].randomMouseUnits)
        {
            StartCoroutine(SpawnRandomMouseUnitWithDelay(randomMouseUnit));
        }
    }
    

    /// <summary>Spawning one type of mouse, uses the Json to know the spaces between each mouse</summary>
    /// <remarks>Maintained by: Emily</remarks>
    IEnumerator SpawnMouseUnitWithDelay(MouseUnit mouseUnit)
    {
        yield return new WaitForSeconds(mouseUnit.timeOfWave);
        for (int i = 0; i < mouseUnit.amount; i++)
        {
            SpawnMouse(GetMouseType(mouseUnit.type));
            yield return new WaitForSeconds(mouseUnit.frequency);
        }
    }
    
    /// <summary>Spawns mouse mouse unit using parameters specified in JSON</summary>
    /// <remarks>Maintained by: Antosh</remarks>
    IEnumerator SpawnRandomMouseUnitWithDelay(RandomMouseUnit mouseUnit)
    {
        yield return new WaitForSeconds(mouseUnit.timeOfWave);
        for (int i = 0; i < mouseUnit.amount; i++)
        {
            SpawnMouse(GetRandomMouse(mouseUnit.difficulty));
            yield return new WaitForSeconds(mouseUnit.frequency);
        }
    }

    private MiceScriptableObject GetRandomMouse(string mouseUnitDifficulty)
    {
        List<MiceScriptableObject> sameDifficultyMice = GetAllMice(mouseUnitDifficulty);
        return sameDifficultyMice[Random.Range(0, sameDifficultyMice.Count)];
    }

    
    private List<MiceScriptableObject> GetAllMice(string mouseUnitDifficulty)
    {
        List<MiceScriptableObject> sameDifficultyMice = new List<MiceScriptableObject>();
        foreach (var mouseType in mouseTypesList)
        {
            if (String.Equals(mouseType.difficulty.ToString(), mouseUnitDifficulty,
                    StringComparison.CurrentCultureIgnoreCase)) 
            {
                sameDifficultyMice.Add(mouseType);
            }
        }
        
        return sameDifficultyMice;

    }
}