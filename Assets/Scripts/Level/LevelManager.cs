using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameManagement;
using Level.WaveData;
using Level.WaveData.WaveData;
using Mouse;
using Music;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class LevelManager : MonoBehaviour
{
    public static LevelManager LM;

    private Waves waves;
    private int currentWaveNum = 0;
    private int miceToBeReleased = -1; //set to -1 so that game doesnt automatically transition into next wave

    private WaveTextManager waveTextManager;
    [SerializeField] private GameObject victoryScreen;

    [FormerlySerializedAs("level")] [SerializeField] public int currentLevel;

    private void Awake()
    {
        LM = this;
        waveTextManager = GetComponent<WaveTextManager>();
    }

    void Start()
    {
        LoadLevel();
        StartCoroutine(StartWaveWithText());
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
        TextAsset jsonFile = Resources.Load("Waves/level" + currentLevel) as TextAsset;
        waves = JsonUtility.FromJson<Waves>(jsonFile.text);
    }


    public IEnumerator FadeInAndOut(SpriteRenderer sprite)
    {
        while (GetAlpha(sprite) < 1)
        {
            ChangeAlpha(sprite, GetAlpha(sprite) + 0.01f);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(3);
        StartCoroutine(FadeOut(sprite, 0));
    }

    private IEnumerator FadeOut(SpriteRenderer sprite, float targetAlpha)
    {
        while (GetAlpha(sprite) >= targetAlpha)
        {
            ChangeAlpha(sprite, GetAlpha(sprite) - 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
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
        var currentWaveObj = waves.waves[currentWaveNum];
        miceToBeReleased = 0;

        //prepare deployment of standard mouse units
        foreach (var mouseUnit in currentWaveObj.mouseUnits)
        {
            StartCoroutine(SpawnMouseUnitWithDelay(mouseUnit));
            miceToBeReleased += mouseUnit.amount;
        }

        //prepare deployment of random mouse units with variation of mice in them
        if (currentWaveObj.randomMouseUnits != null)
        {
            foreach (var randomMouseUnit in currentWaveObj.randomMouseUnits)
            {
                StartCoroutine(SpawnRandomMouseUnitWithDelay(randomMouseUnit));
                miceToBeReleased += randomMouseUnit.amount;
            }
        }
    }

    /// <summary>
    /// Displays necessary text and then starts next wave
    /// </summary>
    /// <remarks>maintained by Antosh</remarks>
    private IEnumerator TransitionIntoNextWave()
    {
        currentWaveNum++;
        if (currentWaveNum == waves.waves.Length) //check that the final wave has just happened
        {
            yield return new WaitForSeconds(1.5f);
            GameManager.isPaused = true;
            Time.timeScale = 0.0f;
            SoundPlayer.instance.PlayVictoryFX();
            victoryScreen.SetActive(true);
        }
        else
        {
            yield return StartWaveWithText();
        }
    }

    private IEnumerator StartWaveWithText()
    {
        yield return new WaitForSeconds(1.5f);
        yield return waveTextManager.DisplayStartingWaveText(currentWaveNum);
        StartWave();
    }


    /// <summary>Spawning one type of mouse, uses the Json to know the spaces between each mouse</summary>
    /// <remarks>Maintained by: Emily</remarks>
    private IEnumerator SpawnMouseUnitWithDelay(MouseUnit mouseUnit)
    {
        yield return new WaitForSeconds(mouseUnit.timeOfWave);
        for (int i = 0; i < mouseUnit.amount; i++)
        {
            MouseFactory.Instance.SpawnMouse(mouseUnit.type);
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
            MouseFactory.Instance.SpawnMouse(mouseUnit.difficulty);
            miceToBeReleased--;
            yield return new WaitForSeconds(mouseUnit.frequency);
        }
    }
}