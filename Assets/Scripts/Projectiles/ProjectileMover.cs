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

    void Update()
    {
        transform.position += transform.up * Time.deltaTime * projectileSpeed;      // update position

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