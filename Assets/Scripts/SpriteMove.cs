using UnityEditor;
using UnityEngine;

public class SpriteMove : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    private Transform[] targets;
    private Transform target;
    private int index = 0;

    public float totalDistanceMoved;
    private float mouseDamage= 10; //The amount of damage that particular mouse causes to the player
    public HealthManager health;

    // Temporary mouse stats
    private string mouseName;
    private float speed;
    private float mouseHealth;
    private float size;
    private Sprite sprite;
    private bool canGhost;

    void Start()
    {
        health= GameObject.FindGameObjectWithTag("Health").GetComponent<HealthManager>(); //finds the health manager
        moveSpeed = 10;
        targets = LevelManager.LM.TurningPoints;
        target = targets[index];
        
    }

    void Update()
    {   
        if (Vector2.Distance(target.position, transform.position) <= 0.1f){
            index++;
            if (index==targets.Length){
                Destroy(gameObject);
                health.TakeDamage(mouseDamage);//the player taking damage as the mouse reaches the end
            }else{
                target = targets[index];
            }
        }
    }

    void FixedUpdate(){
        var direction = Vector3.Normalize(target.position - transform.position);
        var movement = direction * moveSpeed * Time.deltaTime;
        transform.position += movement;
        totalDistanceMoved += movement.magnitude;
    }

    /// <summary> Puts stats into relevant variables from a given ScriptableObject </summary>
    /// <param name = "mouseStats"> MiceScriptableObject containing stats for that mouse type </param>
    /// <remarks>Maintained by: Ben Brixton </remarks>
    public void loadStats(MiceScriptableObject mouseStats){
        mouseName = mouseStats.mouseName;
        speed = mouseStats.speed;
        mouseHealth = mouseStats.health;
        size = mouseStats.size;
        sprite = mouseStats.sprite;
        canGhost = mouseStats.canGhost;

        Debug.Log(mouseName);       // TEMPORARY: used to check different mouse types are being spawned, with relevant stats
    }
}
