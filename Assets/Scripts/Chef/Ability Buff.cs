using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBuff : MonoBehaviour
{
    Collider2D[] colliders;
    public float ATK = 0;
    public float ATKSpeed= 0;
    // Start is called before the first frame update
    void Start()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, 3);
    }

    // Update is called once per frame
    void Update()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, 3);
        foreach(Collider2D collider in colliders){
            GameObject temp = collider.gameObject;
            if(temp!=this && temp.tag=="Chef"){
                DamageFactor df = temp.GetComponent<DamageFactor>();
                df.damage = df.damage*(1f+ATK);
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
