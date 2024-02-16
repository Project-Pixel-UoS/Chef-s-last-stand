using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class LevelManager : MonoBehaviour
{
    public static LevelManager LM;
    public Transform[] TurningPoints;
    public GameObject enemy;
    public MiceScriptableObject[] mouseTypesList;
    private Waves waves;
    
    private int currentWave = 0;
    private int miceToBeReleased = 0;

    [SerializeField] private Text waveText;

    void Start()
    {
        LM = this;
        waveText.enabled = false;
        LoadLevel();
        StartWave();
    }

    private void Update()
    {
        //check that all balloons to be spawned have been spawned, and there are no mice on the map
        if (miceToBeReleased == 0 && !GameObject.FindWithTag("Mouse"))
        {
            miceToBeReleased--; //decrement mice to be released so that OnWaveFinished() is not triggered again
            StartCoroutine(TransitionIntoNextWave());
        }
    }



    /// <summary>fills the waves list with data from JSON file</summary>
    /// <remarks>Maintained by: Antosh</remarks>
    private void LoadLevel()
    {
        TextAsset jsonFile = Resources.Load("Waves/waves") as TextAsset;
        waves = JsonUtility.FromJson<Waves>(jsonFile.text);
        // GetBalloonsToBeReleased();
    }


    /// <summary>Spawns a mouse of indicated mouse type</summary>
    /// <param name="mouseType"> type of mouse that will be spawned in</param>
    /// <remarks>Maintained by: Emily</remarks>
    private void SpawnMouse(MiceScriptableObject mouseType)
    {
        GameObject newMouse =
            Instantiate(enemy, TurningPoints[0].position, transform.rotation); // Instantiate mouse prefab
        newMouse.GetComponent<MouseStats>().loadStats(mouseType);
        miceToBeReleased--;
    }

    /// <summary>
    /// returns mice scriptable object that corresponds to the mouseName provided
    /// </summary>
    /// <exception cref="ArgumentException"> thrown if mouseName does not correspond to any type of mouse</exception>
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


    /// <summary>The process of one wave</summary>
    /// <remarks>Maintained by: Emily</remarks>
    private void StartWave()
    {
        miceToBeReleased = 0;

        //prepare deployment of standard mouse units
        foreach (var mouseUnit in waves.waves[currentWave].mouseUnits)
        {
            StartCoroutine(SpawnMouseUnitWithDelay(mouseUnit));
            miceToBeReleased += mouseUnit.amount;
        }

        //prepare deployment of random mouse units with variation of mice in them
        foreach (var randomMouseUnit in waves.waves[currentWave].randomMouseUnits)
        {
            StartCoroutine(SpawnRandomMouseUnitWithDelay(randomMouseUnit));
            miceToBeReleased += randomMouseUnit.amount;
        }
    }
    
    IEnumerator TransitionIntoNextWave()
    {
        yield return DisplayFinishedWaveText();
        currentWave++;
        if (currentWave == waves.waves.Length) //check that the final wave has just happened
        {
            yield return DisplayLevelComplete();
        }
        else
        {
            yield return DisplayStartingWaveText();
        }
    }
    
    IEnumerator  DisplayLevelComplete()
    {
        yield return new WaitForSeconds(1);
        waveText.enabled = true;
        waveText.text = "Level Complete!";
    }

    IEnumerator  DisplayFinishedWaveText()
    {
        yield return new WaitForSeconds(1);
        waveText.text = "Wave Finished";
        waveText.enabled = true;
        yield return new WaitForSeconds(3);
        waveText.enabled = false;
        yield return new WaitForSeconds(1);
    }

    IEnumerator DisplayStartingWaveText()
    {
        waveText.text = "Wave " + (currentWave + 1) + " Starting";
        waveText.enabled = true;
        yield return new WaitForSeconds(3);
        waveText.enabled = false;
        yield return new WaitForSeconds(1);
        StartWave();
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