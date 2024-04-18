using System.Collections;
using System.Collections.Generic;
using GameManagement;
using UnityEngine;

public class SpeedButton : MonoBehaviour
{

    [SerializeField] float multiplier;

    /// <summary> Toggles time dependent processes' speeds </summary>
    /// <remarks> Maintained by: Ben Brixton </remarks>
    public void ToggleSpeed(){
        if(GameManager.isPaused){ return; }
        Time.timeScale = Mathf.Approximately(Time.timeScale, 1.0f) ? multiplier : 1.0f;       // Flips timescale between 1x and ?x
    }
}
