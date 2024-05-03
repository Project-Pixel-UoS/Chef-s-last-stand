using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputManager
{
    
    private Vector2 fingerDownPosition;
    
    
    private void Update()
    {
        foreach (Touch touch in Input.touches) //todo remove for each loop because we cant have 2 swipes at a time i think?
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerDownPosition = touch.position;
            }

            if (touch.phase is TouchPhase.Began)
            {
                
            }



            // if (touch.phase is TouchPhase.Ended or TouchPhase.Moved && touch.phase != TouchPhase.Stationary)
            // {
            //     DetectInput(touch);
            // }

            // if (touch.phase is TouchPhase.Ended)
            // {
            //     canPerformAction = true;
            // }
        }
    }
}