using System.Collections;
using System.Collections.Generic;
using GameManagement;
using Level.WaveData;
using UnityEngine;

public class SpeedButton : MonoBehaviour
{

    private ButtonState currentButtonState = ButtonState.IncreaseSpeed;

    /// <summary> Toggles time dependent processes' speeds </summary>
    /// <remarks> Maintained by: Ben Brixton </remarks>
    public void ToggleSpeed(){

        if(GameManager.isPaused) return;
        
        if (currentButtonState == ButtonState.DecreaseSpeed)
        {
            DecreaseSpeed();
        }
        else
        {
            IncreaseSpeed();
        }
    }

    private void IncreaseSpeed()
    {
        TimeScaleManager.instance.SpeedMultiplier++;
        if (TimeScaleManager.instance.SpeedMultiplier == 3)
        {
            currentButtonState = ButtonState.DecreaseSpeed;
            ReverseButtonArrows();
        }
    }

    private void DecreaseSpeed()
    {
        TimeScaleManager.instance.SpeedMultiplier--;
        if (TimeScaleManager.instance.SpeedMultiplier == 1)
        {
            currentButtonState = ButtonState.IncreaseSpeed;
            ReverseButtonArrows();
        }
    }

    private void ReverseButtonArrows()
    {
        transform.rotation = 
            Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 180);
    }
}
    
public enum ButtonState
{
    IncreaseSpeed,
    DecreaseSpeed
}
