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
    private int amountOfTypesOfMice=3;//this needs to be changed when more mice are added!
    private int[] amountOfEachMouse={0,0,0};// when more mice are added need to add more zeros to this as well
    private float[] timeDelayOfEachMouse={0,0,0};//same with this one
    private int miceSpawned;

  
    
    
    void Start()
    {
        LM = this;
        LoadLevel();
        //InvokeRepeating("spawnMouse", 0, 5);
        StartCoroutine(wave());
    }
   
   

    /// <summary>fills the waves list with data from JSON file</summary>
    /// <remarks>Maintained by: Antosh</remarks>
    private void LoadLevel()
    {
        TextAsset jsonFile = Resources.Load("Waves/waves") as TextAsset;
        waves = JsonUtility.FromJson<Waves>(jsonFile.text);
        Debug.Log(waves.waves[currentWave].mouseUnits[0].timeOfWave);
    }

    private void spawnMouse(int mouseType)
    {
        
        GameObject newMouse =
            Instantiate(enemy, TurningPoints[0].position, transform.rotation); // Instantiate mouse prefab
        newMouse.GetComponent<MouseStats>().loadStats(mouseTypesList[mouseType]);
    }

    
    /// <summary>Puts the extra mice in random catogaries and sorts out amountOfEachMouse to be correct</summary>
    /// <remarks>Maintained by: Emily</remarks>

    private void chooseRandom(int amount)
    {
        int typeOfMouse;
        for(int i=0;i<amountOfTypesOfMice;i++){
            amountOfEachMouse[i]= waves.waves[currentWave].mouseUnits[i].amount;
            timeDelayOfEachMouse[i]=waves.waves[currentWave].mouseUnits[i].timeOfWave; 

        }
        for(int i=0;i<amount;i++ ){//this will have to be changed to depend on difficulty of random mice.
            typeOfMouse=Random.Range(0,amountOfTypesOfMice);
            amountOfEachMouse[typeOfMouse]++;
        }

    }

     /// <summary>The process of one wave</summary>
    /// <remarks>Maintained by: Emily</remarks>

   IEnumerator wave()
    {
        miceSpawned=0;
        float startTime=Time.time;//Time the wave begins
        int miceInWave= waves.waves[currentWave].totalMice;//number of mice that will be spawned in the wave
        bool[] started={false,false,false};//needs to be as long as the type of mice there are! 

        chooseRandom(waves.waves[currentWave].randomMouseUnits[0].amount);
        //Debug.Log(miceSpawned);  
        while(miceSpawned<miceInWave){
            for(int i=0;i<amountOfTypesOfMice;i++){
                float timeNow=Time.time;
                if((timeNow-startTime)>timeDelayOfEachMouse[i] && started[i]==false){
                   StartCoroutine(spawnMouseUnit(amountOfEachMouse[i],i));
                    started[i]=true;
                }
                yield return null;
            }

            
        }
        Debug.Log("All Mice have been Spawned Wave: "+currentWave);
    }
     /// <summary>Spawning one type of mouse, uses the Json to know the spaces between each mouse</summary>
    /// <remarks>Maintained by: Emily</remarks>
    IEnumerator spawnMouseUnit(int amountOfOneMouse,int whichMouse)
        {
        for(int i = 0; i < amountOfOneMouse; i ++){
        spawnMouse(whichMouse);
        miceSpawned++;
        yield return new WaitForSeconds(waves.waves[currentWave].mouseUnits[whichMouse].frequency);
        }
        }
}
    


