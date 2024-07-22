using Level.WaveData.WaveData;

using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Mouse_", menuName = "ScriptableObjects/MiceScriptableObject", order = 1)]
public class MiceScriptableObject : ScriptableObject
{
    public string mouseName;
    public float speed;
    public float health;
    public float size;
    public Sprite sprite;
    public bool canGhost;
    public string splitMouseType = ""; // mouse type that is produced upon death. Empty string if none 
    public int numOfSplitMice; // amount of mice produced upon death. Set to 0 if none
    public bool armoured;
    public MouseDifficulty difficulty;
    

}