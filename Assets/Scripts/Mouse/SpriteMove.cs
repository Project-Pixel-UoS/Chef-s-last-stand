using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpriteMove : MonoBehaviour
{
    private MouseStats stats;
    private Transform[] targets;
    private Transform target;
    private int index = 0;

    public float totalDistanceMoved;
    private float mouseDamage = 10; //The amount of damage that particular mouse causes to the player
    public HealthManager health;



    void Start()
    {
        health = GameObject.FindGameObjectWithTag("Health").GetComponent<HealthManager>(); //finds the health manager
        stats = gameObject.GetComponent<MouseStats>();
        targets = LevelManager.LM.TurningPoints;
        target = targets[index];
    }

    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            index++;
            if (index == targets.Length)
            {
                Destroy(gameObject);
                health.TakeDamage(mouseDamage); //the player taking damage as the mouse reaches the end
            }
            else
            {
                target = targets[index];
            }
        }
    }

    void FixedUpdate()
    {
        var direction = Vector3.Normalize(target.position - transform.position);
        var movement = direction * stats.speed * Time.deltaTime;
        transform.position += movement;
        totalDistanceMoved += movement.magnitude;
    }



}