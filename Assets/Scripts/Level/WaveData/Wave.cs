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

    public Wave(int waveId, MouseUnit[] mouseUnits, RandomMouseUnit[] randomMouseUnits)
    {
        this.waveId = waveId;
        this.mouseUnits = mouseUnits;
        this.randomMouseUnits = randomMouseUnits ?? Array.Empty<RandomMouseUnit>();
    }
}