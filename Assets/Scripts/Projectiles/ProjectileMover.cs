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
    [SerializeField] private float destroyTime = 1; // time until projectile is destroyed (seconds)

    void Update()
    {
        MoveProjectile(15);
        DestroyIfTimerIs0();
    }

    private void DestroyIfTimerIs0()
    {
        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile(int speed)
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mouse"))
        {
            Destroy(gameObject);
        }
    }
}