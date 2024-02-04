using System;



/// <summary>
/// Responsible for storing data needed to deploy a unit of mice of the same difficulty from JSON file
/// </summary>
/// <remarks>Antosh</remarks>
[Serializable]
public class RandomMouseUnitUnit : SerializableMouseUnit
{
    public string difficulty;
}