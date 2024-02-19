using System;
using Level.WaveData.WaveData;


/// <summary>
/// Responsible for storing data needed to deploy a unit of mice of the same difficulty from JSON file
/// </summary>
/// <remarks>Antosh</remarks>
[Serializable]
public class RandomMouseUnit : SerializableMouseUnit
{
    public RandomMouseUnit(string difficulty)
    {
        Enum.TryParse(difficulty, out this.difficulty);
    }

    public MouseDifficulty difficulty;

}