using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAOE : MonoBehaviour
{
    [SerializeField] private float range; // range at which chef can attack mice
    [SerializeField] private float cooldown; // time in between chef shooting (seconds)
    private float cooldownTimer; // timer for cooldown in between shots
    private DamageFactor damageFactor; // damage factor

    void Start(){
        damageFactor = GetComponent<DamageFactor>();
    }

        void Update(){
        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
        AOE();
    }


    /// <returns>
    /// mice in range of the chef
    /// </returns>
    /// <remarks> maintained by: Antosh </remarks>
    private List<GameObject> GetMiceInRange()
    {
        var mice = GameObject.FindGameObjectsWithTag("Mouse");
        var miceInRange = new List<GameObject>();
        foreach (var mouse in mice)
        {
            float distance = (mouse.transform.position - transform.position).magnitude;
            if (distance <= range)
            {
                miceInRange.Add(mouse);
            }
        }

        return miceInRange;
    }

    /// <summary> Hits all mice in range </summary>
    /// <remarks> maintained by: Antosh </remarks>
    private void AOE(){
        if(cooldownTimer > 0) return;

        foreach(GameObject mouse in GetMiceInRange()){
            StartCoroutine(mouse.GetComponent<DamageHandler>().TakeDamage(damageFactor));
        }

        cooldownTimer = cooldown;
    }


}
