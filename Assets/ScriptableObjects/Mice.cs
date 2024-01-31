using UnityEngine;

[CreateAssetMenu(fileName = "Mouse_", menuName = "ScriptableObjects/MiceScriptableObject", order = 1)]
public class MiceScriptableObject : ScriptableObject
{
    public string mouseName;
    public float speed;
    public float health;
    public float size;
    public Sprite sprite;
    public bool canGhost;

    public MouseDifficulty difficulty;

    public enum MouseDifficulty
    {
        Low,
        Medium,
        High
    }

}