using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager MM;
    public GameObject MapPrefab;
    private GameObject[] allMap;
    private Vector3 pos = new Vector3(-10,-15,0);
    /// <todo>
    /// Detect if the chef is already existed.
    /// </todo>
    [HideInInspector] public Vector3[] allPos;
    [HideInInspector] public Vector3 targetMapPos;

    /// <summary> Instantiate map tiles by using list of coordinates and stored in list.</summary>
    /// <param name = "allMap"> stores the all map tiles</param>
    /// <remarks>Maintained by: Lishan Xu</remarks>
    void Start()
    {
        allMap = new GameObject[1];
        for(int  i = 0; i < 1; i++) 
        {
            allMap[i] = Instantiate(MapPrefab, pos, transform.rotation);
        }
        MM = this;
    }

    /// <summary> Get the current overlapped map tile and update its position.</summary>
    /// <remarks>Maintained by: Lishan Xu</remarks>
    void Update()
    {
        foreach (GameObject i in allMap){
            if (i.GetComponent<TargetMap>().IsOverlapping()){
                targetMapPos = i.transform.position;
            }
        }

    }
}
