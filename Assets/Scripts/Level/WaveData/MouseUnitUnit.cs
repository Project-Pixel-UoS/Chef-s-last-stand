using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Responsible for storing data needed to deploy a unit of mice of the same type from JSON file
/// </summary>
/// <remarks>Antosh</remarks>
[Serializable]
public class MouseUnitUnit : SerializableMouseUnit
{
    public string type;
}
