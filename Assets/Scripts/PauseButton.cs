using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{

    /// <summary> Toggles time dependent processes (pauses the game) </summary>
    /// <remarks> Maintained by: Ben Brixton </remarks>
    public void TogglePause(){
        Time.timeScale = Mathf.Approximately(Time.timeScale, 0.0f) ? 1.0f : 0.0f;       // Flips timescale between 0 and 1
    }
}
