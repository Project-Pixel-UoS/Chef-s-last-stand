using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Responsible for moving projectile, and destruction
/// </summary>
/// <remarks>Author: Ben</remarks>
public class ProjectileMover : MonoBehaviour
{
    public float projectileSpeed; // speed of projectile
    [SerializeField] private float destroyTime; // time until projectile is destroyed (seconds)

    // public Vector3 direction;
    private void Start()
    {
        // print("TRANSLATING PROJETILE");
        // print("transform up: " + transform.forward );
    }

    void Update()
    {
        // transform.Translate(transform.forward  * Time.deltaTime * projectileSpeed);

        // if (direction == null)
        // {
        //     throw new InvalidOperationException("direction must be set before creating the projectile");
        // }

        // print("direction: " + direction);
        // print("projectile speed: " + projectileSpeed);

        //problem is when chef
        // transform.Translate(transform.up  * Time.deltaTime * projectileSpeed);
        // print("MOVE VECTOR: " + -transform.up);
        // transform.position += (transform.up * 0.01f); // backwards shoot works
        // transform.Translate(transform.up * 0.01f); //backwards shoot doesnt work, goes off to the side
        // transform.position += (transform.up * Time.deltaTime); // backwards shoot works

        print(Time.deltaTime);
        Vector3 move = (transform.up * Time.deltaTime); // backwards shoot works
        move.x *= -1;
        move.y *= -1;


        transform.position += -move; // backwards shoot works


        // transform.position -= (transform.up * 0.01f);
        // * Time.deltaTime * projectileSpeed;      // update position

        // destroy game object when timer reaches zero
        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    // if projectile collides with game object, it is destroyed
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mouse"))
        {
            Destroy(gameObject);
        }
    }
}