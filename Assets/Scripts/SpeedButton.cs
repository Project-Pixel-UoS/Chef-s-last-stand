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

        TMPro.TextMeshProUGUI textObject = GetComponentInChildren<TMPro.TextMeshProUGUI>();

        if(Mathf.Approximately(Time.timeScale, 1.0f)){
            GameManager.speedMultiplier = multiplier;
            textObject.text = "Speed: " + multiplier + "x";
            Time.timeScale = GameManager.speedMultiplier;
        }
        else{
            GameManager.speedMultiplier = 1.0f;
            textObject.text = "Speed: 1x";
            Time.timeScale = GameManager.speedMultiplier;
        }
    }
}
