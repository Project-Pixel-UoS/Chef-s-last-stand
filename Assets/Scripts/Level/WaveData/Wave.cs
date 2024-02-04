using System;
using System.Collections;
using System.Collections.Generic;
using Level;
using UnityEngine;


/// <summary>
/// Responsible for storing wave data from JSON file
/// </summary>
/// <remarks>Antosh</remarks>
[Serializable]
public class Wave
{
    public int waveId;
    public MouseUnitUnit[] mouseUnits;
    public RandomMouseUnitUnit [] randomMouseUnits;


}