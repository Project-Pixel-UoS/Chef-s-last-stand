using System.Collections;
using System.Collections.Generic;
using Chef;
using Mouse;
using Unity.Collections;
using UnityEngine;

/// <summary>
/// Component should be attatched to the head chef who has the ability to buff other chefs
/// </summary>
/// <remarks> author: Lishan</remarks>
public class AbilityBuff : MonoBehaviour
{
    Collider2D[] colliders;
    public float ATK = 0;
    public float ATKSpeed = 0;
    public float rangeIncrease = 2;

    private float range;

    void Start()
    {
        range = GetComponent<Range>().Radius;
        colliders = Physics2D.OverlapCircleAll(transform.position, range);
    }

    void Update()
    {
        // Get all objects within range
        colliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D collider in colliders)
        {
            GameObject gameObject = collider.gameObject; // Get game object

            Buff buff = gameObject.GetComponent<Buff>(); // Get its "buff" object

            // If it has no "buff" object, it isn't a chef
            if (buff != null)
            {
                // If it has a "buff" object, set its stats
                buff.damageIncrease = ATK;
                buff.speedIncrease = ATKSpeed;
                buff.rangeIncrease = rangeIncrease;
            }
        }
    }
}