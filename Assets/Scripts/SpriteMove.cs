
using UnityEngine;

public class SpriteMove : MonoBehaviour
{
    // private Transform endPoint;
    private float moveSpeed;
    private Transform[] targets;
    private Transform target;
    private int index = 0;

    public float totalDistanceMoved;

    void Start()
    {
        // endPoint = LevelManager.LM.endPoint;
        moveSpeed = LevelManager.LM.moveSpeed;
        targets = LevelManager.LM.TurningPoints;
        target = targets[index];
    }

    void Update()
    {   
        if (Vector2.Distance(target.position, transform.position) <= 0.1f){
            index++;
            if (index==targets.Length){
                Destroy(gameObject);
            }else{
                target = targets[index];
            }
        }
        // Vector3 direction = endPoint.position - transform.position;
        // if (Vector2.Distance(endPoint.position, transform.position) <= 0.1f){
        //     Destroy(gameObject);
        // }
        //
    }

    void FixedUpdate(){
        var direction = Vector3.Normalize(target.position - transform.position);
        var movement = direction * moveSpeed * Time.deltaTime;
        transform.position += movement;
        totalDistanceMoved += movement.magnitude;
    }
}
