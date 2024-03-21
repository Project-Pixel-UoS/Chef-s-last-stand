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

    // Update is called once per frame
    void Update()
    {   
        
    }

    // if projectile collides with game object, it is destroyed
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mouse"))
        {
            Destroy(gameObject);
            parentChef.removePlate();
        }
    }

    

}
