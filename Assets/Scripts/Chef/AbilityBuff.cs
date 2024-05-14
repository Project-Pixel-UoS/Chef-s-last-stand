using System.Collections;
using System.Collections.Generic;
using Chef;
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
        colliders = Physics2D.OverlapCircleAll(transform.position, range);
        if (colliders != null)
        {
            foreach (Collider2D collider in colliders)
            {
                GameObject gameObject = collider.gameObject;
                if (gameObject.CompareTag("HeadChef"))
                {
                    Buff buff = gameObject.GetComponent<Buff>();
                    if (buff == null)
                    {
                        gameObject.AddComponent<Buff>();
                    }

                    buff = gameObject.GetComponent<Buff>();
                    buff.damageIncrease = ATK;
                    buff.speedIncrease = ATKSpeed;
                    buff.rangeIncrease = rangeIncrease;
                }
            }
        }
    }


}