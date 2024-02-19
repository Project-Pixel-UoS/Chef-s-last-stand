using System;

/// <summary>
/// Responsible for storing data from JSON file used to spawn in a pack of mice
/// </summary>
/// <remarks>Antosh</remarks>

[Serializable]
public abstract class SerializableMouseUnit
{
    public int amount; // num of mice in the pack
    public float frequency; // time between release of each mouse
    public float timeOfWave; // num of secs since beginning of wave, for this mouse unit to be released
    
    
}