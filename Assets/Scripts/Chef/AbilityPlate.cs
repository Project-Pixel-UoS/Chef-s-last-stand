using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chef;

public class AbilityPlate : MonoBehaviour
{
    [SerializeField] private GameObject Projectile; // projectile for chef to shoot
    [SerializeField] private float cooldown; // time in between chef shooting (seconds)
    [SerializeField] private float maxPlates;
    public GameObject[] turningPoints;
    private float cooldownTimer; // timer for cooldown in between shots
    private float currPlates;
    private Range range;
    // Start is called before the first frame update
    void Start()
    {
        turningPoints = GameObject.FindGameObjectsWithTag("TurningPoints");
    }
    private void Awake()
    {
        range = GetComponent<Range>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Projectile == null) return;
        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
        if (currPlates != maxPlates) Shoot();
    }

    /// <returns>
    /// turning points in range of the chef
    /// </returns>
    private List<GameObject> GetTPsInRange()
    {
        var tpsInRange = new List<GameObject>();
        foreach (var turningpoint in turningPoints)
        {
            float distance = (turningpoint.transform.position - transform.position).magnitude;
            if (distance <= range.radius)
            {
                tpsInRange.Add(turningpoint);
            }
        }
        return tpsInRange;
    }

    /// <returns>
    /// a possible plate position on the map
    /// </returns>
    /// <remarks> maintained by: Martin </remarks>
    private Vector2 getPlatePosition()
    {
        //grab random TP in range
        var inRangeTPs = GetTPsInRange();
        if (inRangeTPs.Count == 0) return Vector2.zero;
        int rndIndex = Random.Range(0, inRangeTPs.Count);

        int tpIndex = System.Array.IndexOf(turningPoints, inRangeTPs[rndIndex]);
        var adjTPs = turningPoints[(tpIndex-1)..(tpIndex+2)]; //get prev. and next TPs of random TP
        rndIndex = Random.Range(0, adjTPs.Length - 1); //select random 2 TPs to place plate btw.
        adjTPs = adjTPs[(rndIndex)..(rndIndex + 2)];
        var vectorRange = adjTPs[1].transform.position - adjTPs[0].transform.position;

        var platePos = new Vector2();
        var found = false;
        while (!found)
        {
            var randomPos = adjTPs[0].transform.position + Random.value * vectorRange;
            if ((randomPos - transform.position).magnitude < range.radius)
            {
                platePos = randomPos;
                found = true;
            }
        }
        return platePos;
    }

    private void Shoot()
    {
        if (cooldownTimer > 0) return;
        cooldownTimer = cooldown;
        if (getPlatePosition().x != 0)
        {
            var plate = Instantiate(Projectile, getPlatePosition(), transform.rotation);
            plate.transform.parent = transform;
            currPlates++;
        }
        
    }

    public void removePlate()
    {
        currPlates--;
    }
}
