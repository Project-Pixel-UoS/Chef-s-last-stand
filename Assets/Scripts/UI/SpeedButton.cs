using System.Collections;
using System.Collections.Generic;
using GameManagement;
using Level.WaveData;
using Music;
using UnityEngine;
using UnityEngine.UI;

public class SpeedButton : MonoBehaviour
{

    private ButtonState currentButtonState = ButtonState.IncreaseSpeed;

    /// <summary> Toggles time dependent processes' speeds </summary>
    /// <remarks> Maintained by: Ben Brixton </remarks>
    public void ToggleSpeed(){

        if(GameManager.isPaused) return;
        SoundPlayer.instance.PlayButtonClickFX();

        if (currentButtonState == ButtonState.DecreaseSpeed)
        {
            DecreaseSpeed();
        }
        else
        {
            IncreaseSpeed();
        }
        
        gameObject.GetComponentInChildren<Text>().text = "Speed: " + TimeScaleManager.instance.SpeedMultiplier + "x";

    }

    private void IncreaseSpeed()
    {
        TimeScaleManager.instance.SpeedMultiplier++;
        if (TimeScaleManager.instance.SpeedMultiplier == 3)
        {
            currentButtonState = ButtonState.DecreaseSpeed;
        }
    }

    private void DecreaseSpeed()
    {
        TimeScaleManager.instance.SpeedMultiplier--;
        if (TimeScaleManager.instance.SpeedMultiplier == 1)
        {
            currentButtonState = ButtonState.IncreaseSpeed;
        }
    }
    
}
    
public enum ButtonState
{
    IncreaseSpeed,
    DecreaseSpeed
}
