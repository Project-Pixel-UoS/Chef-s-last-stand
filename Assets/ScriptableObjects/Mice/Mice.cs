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
    public bool canSplit;
    public bool armoured;
    [FormerlySerializedAs("healing")] public bool canHeal;
    public MouseDifficulty difficulty;

}