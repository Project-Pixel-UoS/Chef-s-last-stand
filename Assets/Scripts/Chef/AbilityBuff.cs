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

    // Start is called before the first frame update
    void Start()
    {
        range = GetComponent<Range>().Radius;
        colliders = Physics2D.OverlapCircleAll(transform.position, range);
    }

    // Update is called once per frame
    void Update()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, range);
        if (colliders != null)
        {
            foreach (Collider2D collider in colliders)
            {
                GameObject gameObject = collider.gameObject;
                if (gameObject.CompareTag("Chef"))
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

    // private void OnCollisionEnter2D(Collision2D collision){
    //     Debug.Log("1");
    // }

    // private void OnCollisionStay2D(Collision2D collision){
    //     Debug.Log("2");
    // }

    // private void OnCollisionExist2D(Collision2D collision){
    //     Debug.Log("3");
    // }
}