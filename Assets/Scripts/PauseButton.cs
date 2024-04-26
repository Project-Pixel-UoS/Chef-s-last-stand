using System.Collections;
using System.Collections.Generic;
using GameManagement;
using UnityEngine;

public class PauseButton : MonoBehaviour
{

    /// <summary> Toggles time dependent processes (pauses the game) </summary>
    /// <remarks> Maintained by: Ben Brixton </remarks>
    public void TogglePause(){

        TMPro.TextMeshProUGUI textObject = GetComponentInChildren<TMPro.TextMeshProUGUI>();

        if(Mathf.Approximately(Time.timeScale, 0.0f)){
            textObject.text = "Pause";
            GameManager.isPaused = false;
            Time.timeScale = GameManager.speedMultiplier;
        }
        else{
            textObject.text = "Play";
            GameManager.isPaused = true;
            Time.timeScale = 0.0f;
        }
    }


}
