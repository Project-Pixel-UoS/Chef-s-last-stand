using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Responsible for storing wave data from JSON file
/// </summary>
/// <remarks>Antosh</remarks>
[Serializable]
public class Wave
{
    public int waveId;
    public MouseUnit[] mouseUnits;
    public RandomMouseUnit [] randomMouseUnits;


}