using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseStats : MonoBehaviour
{

    // Stats
    public string mouseName;
    public float speed;
    public float health;
    public float size;
    public Sprite sprite;
    public bool canGhost;
    public bool canSplit;

    /// <summary> Puts stats into relevant variables from a given ScriptableObject </summary>
    /// <param name = "mouseStats"> MiceScriptableObject containing stats for that mouse type </param>
    /// <remarks>Maintained by: Ben Brixton </remarks>
    public void loadStats(MiceScriptableObject mouseStats){
        mouseName = mouseStats.mouseName;
        speed = mouseStats.speed;
        health = mouseStats.health;
        size = mouseStats.size;
        sprite = mouseStats.sprite;
        canGhost = mouseStats.canGhost;
        canSplit = mouseStats.canSplit;

        Debug.Log(mouseName);       // TEMPORARY: used to check different mouse types are being spawned with correct stats
    }

    void Start()
    {
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;        // Sets sprite according to scritable object
    }
}
