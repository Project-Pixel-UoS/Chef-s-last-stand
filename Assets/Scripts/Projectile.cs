using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed; // speed of projectile
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
        Destroy(gameObject);
    }
}
