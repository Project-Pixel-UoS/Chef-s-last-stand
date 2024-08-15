using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameManagement;
using Level.WaveData;
using Level.WaveData.WaveData;
using Mouse;
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
    private int miceToBeReleased = -1; //set to -1 so that game doesnt automatically transition into next wave

    private WaveTextManager waveTextManager;

    private bool faded = true; //for route signal coroutine


    private void Awake()
    {
        waveTextManager = GetComponent<WaveTextManager>();
    }

    void Start()
    {
        LM = this;
        LoadLevel();
        StartCoroutine(StartWaveWithText());
        RouteSignal();
    }

    private void Update()
    {
        //check that all balloons to be spawned have been spawned, and there are no mice on the map
        if (miceToBeReleased == 0 && !GameObject.FindWithTag("Mouse"))
        {
            miceToBeReleased--; //decrement mice to be released so that OnWaveFinished() is not triggered again
            if (!GameManager.gameManager.IsGameOver()) // when game over, the wave continues instead of freezing 
            {

                StartCoroutine(TransitionIntoNextWave());
            }
      
        }
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

    /// <summary>Spawns a mouse of indicated mouse type at specified position.</summary>
    private void SpawnMouse(MiceScriptableObject mouseType, Vector3 position, int index)
    {
        GameObject newMouse =
            Instantiate(enemy, position, transform.rotation); // Instantiate mouse prefab
        newMouse.GetComponent<MouseStats>().loadStats(mouseType);
        newMouse.GetComponent<SpriteMove>().SetIndex(index);
    }
    /// <summary>
    /// returns mice scriptable object that corresponds to the mouseName provided
    /// </summary>
    /// <exception cref="ArgumentException"> thrown if mouseName does not correspond to any type of mouse</exception>
    /// <remarks>Maintained by Antosh</remarks>
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

    //enumerator to fade in and out three times
    private void RouteSignal()
    {
        Debug.Log("te");
        foreach (Transform point in TurningPoints)
        {
            SpriteRenderer sprite = point.GetComponent<SpriteRenderer>();
            IEnumerator c = FadeIn(sprite);
            StartCoroutine(c);
        }
    }

    private IEnumerator FadeIn(SpriteRenderer sprite)
    {
        while (GetAlpha(sprite) < 1)
        {
            ChangeAlpha(sprite, GetAlpha(sprite) + 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(3);
        StartCoroutine(FadeOut(sprite));
    }

    private IEnumerator FadeOut(SpriteRenderer sprite)
    {
        while (GetAlpha(sprite) >= 0.1)
        {
            ChangeAlpha(sprite, GetAlpha(sprite) - 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        if (!faded) faded = true;
    }

    public void ChangeAlpha(SpriteRenderer sprite, float alpha)
    {
        var color = sprite.color;
        color.a = alpha;
        sprite.color = color;
    }

    private float GetAlpha(SpriteRenderer sprite)
    {
        return sprite.color.a;
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
    
    /// <summary>
    /// Displays necessary text and then starts next wave
    /// </summary>
    /// <remarks>maintained by Antosh</remarks>
    private IEnumerator TransitionIntoNextWave()
    {
        yield return waveTextManager.DisplayFinishedWaveText();

        currentWave++;
        if (currentWave == waves.waves.Length) //check that the final wave has just happened
        {
            yield return waveTextManager.DisplayLevelComplete();
        }
        else
        {
            yield return StartWaveWithText();
        }
    }

    private IEnumerator StartWaveWithText()
    {
        yield return new WaitForSeconds(1.5f);
        yield return waveTextManager.DisplayStartingWaveText(currentWave);
        StartWave();
    }
    


    /// <summary>Spawning one type of mouse, uses the Json to know the spaces between each mouse</summary>
    /// <remarks>Maintained by: Emily</remarks>
    private IEnumerator SpawnMouseUnitWithDelay(MouseUnit mouseUnit)
    {
        yield return new WaitForSeconds(mouseUnit.timeOfWave);
        for (int i = 0; i < mouseUnit.amount; i++)
        {
            SpawnMouse(GetMouseType(mouseUnit.type));
            miceToBeReleased--;
            yield return new WaitForSeconds(mouseUnit.frequency);
        }
    }

    /// <summary>Spawns mouse mouse unit using parameters specified in JSON</summary>
    /// <remarks>Maintained by: Antosh</remarks>
    private IEnumerator SpawnRandomMouseUnitWithDelay(RandomMouseUnit mouseUnit)
    {
        yield return new WaitForSeconds(mouseUnit.timeOfWave);
        for (int i = 0; i < mouseUnit.amount; i++)
        {
            SpawnMouse(GetRandomMouseType(mouseUnit.difficulty));
            miceToBeReleased--;
            yield return new WaitForSeconds(mouseUnit.frequency);
        }
    }

    
    private MiceScriptableObject GetRandomMouseType(MouseDifficulty mouseUnitDifficulty)
    {
        List<MiceScriptableObject> sameDifficultyMice = GetAllMouseTypes(mouseUnitDifficulty);
        return sameDifficultyMice[Random.Range(0, sameDifficultyMice.Count)];
    }


    /// <summary>
    /// Returns a list of all the mouse types corresponding to the mouse unity difficulty
    /// </summary>
    /// <param name="mouseUnitDifficulty"></param>
    /// <returns></returns>
    private List<MiceScriptableObject> GetAllMouseTypes(MouseDifficulty mouseUnitDifficulty)
    {
        List<MiceScriptableObject> sameDifficultyMice = new List<MiceScriptableObject>();
        foreach (var mouseType in mouseTypesList)
        {
            // if (String.Equals(mouseType.difficulty.ToString(), mouseUnitDifficulty,
            //         StringComparison.CurrentCultureIgnoreCase))
            if (mouseType.difficulty == mouseUnitDifficulty)
            {
                sameDifficultyMice.Add(mouseType);
            }
        }

        return sameDifficultyMice;
    }

    /// <summary>
    /// spawns 2 mice at the trenchcoat mouse's death position.
    /// </summary>
    /// <param name="position">the positions to spawn the mice on.</param>
    /// <param name="index">the next index the split off mice continue to.</param>
    public void SplitMouse(Vector3 position, int index)
    {
        SpawnMouse(GetMouseType("Woody"), position, index);
        SpawnMouse(GetMouseType("Woody"), new Vector3(position.x - 0.5f, position.y), index);
    }
}