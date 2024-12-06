using System;
using System.Collections;
using System.Collections.Generic;
using Chef;
using Mouse;
using Music;
using Range;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

/// <summary>
/// Component should be attatched to the head chef who has the ability to buff other chefs
/// </summary>
/// <remarks> author: Lishan</remarks>
public class AbilityBuff : MonoBehaviour
{
    Collider2D[] colliders;
    public float damageMultiplier = 1; // damage increase multiplier
    public float reloadTimeMultiplier = 1; // speed increase multiplier
    public float rangeMultiplier = 1.5f; // range increase multiplier
    
    

    private float range;
    private CreditManager creditsManager;
    private float passiveIncomeCDTimer;
    [SerializeField] private float cooldown;
    [SerializeField] private int income;
    [SerializeField] private ParticleSystem money;


    void Start()
    {
        range = GetComponent<ChefRange>().Radius;
        colliders = Physics2D.OverlapCircleAll(transform.position, range);
        creditsManager = GameObject.FindGameObjectWithTag("Credits").GetComponent<CreditManager>();

    }

    void Update()
    {
        if (passiveIncomeCDTimer > 0) passiveIncomeCDTimer -= Time.deltaTime;
        HandlePassiveIncome();
        BuffChefs();
    }

    private void BuffChefs()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, range); // Get all objects within range
        foreach (Collider2D collider in colliders)
        {
            Buff buff = collider.gameObject.GetComponent<Buff>();
            if (buff != null) // If it has no "buff" object, it isn't a chef

            {
                buff.DamageMultiplier = damageMultiplier;
                buff.ReloadTimeMultiplier = reloadTimeMultiplier;
                buff.RangeMultiplier = rangeMultiplier;
            }
        }
    }

    private void OnDestroy()
    {
        OnSellChef();
    }

    private void OnSellChef()
    {
        SoundPlayer.instance.PlayButtonClickFX();
        colliders = Physics2D.OverlapCircleAll(transform.position, range); // Get all objects within range
        foreach (Collider2D collider in colliders)
        {
            Buff buff = collider.gameObject.GetComponent<Buff>();
            if (buff != null) // If it has no "buff" object, it isn't a chef
            {
                buff.DamageMultiplier = 1;
                buff.ReloadTimeMultiplier = 1;
                buff.RangeMultiplier = 1;
            }
        }
    }

    

    /// <summary>
    /// checks if head chef is level 3/4, then generate passive income.
    /// </summary>
    private void HandlePassiveIncome()
    {
        if (transform.name.Equals("Chef Head 3(Clone)") || transform.name.Equals("Chef Head 4(Clone)"))
        {
            if (passiveIncomeCDTimer > 0) return;
            creditsManager.IncreaseMoney(income);
            passiveIncomeCDTimer = cooldown;
            money.Play();
            SoundPlayer.instance.PlayMoneyGainedFX();
        }
    }
}