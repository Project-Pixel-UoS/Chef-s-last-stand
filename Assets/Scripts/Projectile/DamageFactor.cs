using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for storing damage stats of projectile
/// </summary>
/// <remarks>Antosh</remarks>
public class DamageFactor : MonoBehaviour
{
    public int damage; 

    //these fields should only be changed for projectiles that deal long lasting damage (eg. poison)
     public float damageRate  = 1; //how often damage is dealt
     public float damageDuration  = 1; //how long lasting this damage will be
}