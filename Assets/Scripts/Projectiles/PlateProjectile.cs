using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateProjectile : MonoBehaviour
{
    private AbilityPlate parentChef;
    // Start is called before the first frame update
    void Start()
    {
        parentChef = GetComponentInParent<AbilityPlate>();
    }

    // if projectile collides with game object, it is destroyed
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mouse"))
        {
            Destroy(gameObject);
            parentChef.removePlate(); //decrease current plate count when destroyed
        }
    }

    

}
