using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chef;
using System.IO;
using Range;
using UnityEngine.Serialization;
using Util;

public class AbilityPlate : MonoBehaviour
{
    [FormerlySerializedAs("Projectile")] [SerializeField] private GameObject projectile; // projectile for chef to shoot
    [SerializeField] private float cooldown; // time in between chef shooting (seconds)
    [SerializeField] private float maxPlates;
    [SerializeField] private Sprite brokenPlate;
    private Transform[] turningPoints;
    private float cooldownTimer; // timer for cooldown in between shots
    private float currPlates;
    private ChefRange chefRange;
    private Buff buff;
    
    // Start is called before the first frame update
    void Start()
    {
        var path = GameObject.Find("Path");
        turningPoints = path.GetComponentsInChildren<Transform>();
    }
    private void Awake()
    {
        chefRange = GetComponent<ChefRange>();
        buff = GetComponent<Buff>();
    }

    void Update()
    {
        if (projectile == null) return;
        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime / buff.ReloadTimeMultiplier ;
        if (currPlates != maxPlates)
        {
            var targetPos = GetPlatePosition();
            var plate = Shoot();
            if (plate != null && targetPos.z != -1)
            {
                StartCoroutine(MovePlate(plate, targetPos));
            }
        }
    }

    /// <returns>
    /// Turning points in range of the chef
    /// </returns>
    private List<Transform> GetTPsInRange()
    {
        var tpsInRange = new List<Transform>();
        foreach (var turningpoint in turningPoints)
        {
            float distance = (turningpoint.transform.position - transform.position).magnitude;
            if (distance <= chefRange.Radius)
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
    private Vector3 GetPlatePosition()
    {
        //grab random TP in range
        var inRangeTPs = GetTPsInRange();
        if (inRangeTPs.Count == 0) return Vector3.back;
        int rndIndex = Random.Range(0, inRangeTPs.Count);

        int tpIndex = System.Array.IndexOf(turningPoints, inRangeTPs[rndIndex]);
        var adjTPs = turningPoints[(tpIndex-1)..(tpIndex+2)]; //get prev. and next TPs of random TP
        rndIndex = Random.Range(0, adjTPs.Length - 1); //select random 2 TPs to place plate btw.
        var restTPs = adjTPs[(rndIndex)..(rndIndex + 2)];
      
        var platePos = new Vector3();
        var found = false;
        while (!found)
        {
            //grabs random position in between the two turning points
            var randomPos = Vector3.Lerp(restTPs[0].position, restTPs[1].position, Random.value);
            if ((randomPos - transform.position).magnitude < chefRange.Radius)
            {
                platePos = randomPos;
                found = true;
            }
        }
        return platePos;
    }

    private GameObject Shoot()
    {
        if (cooldownTimer > 0) return null;
        cooldownTimer = cooldown;
        Utils.PlayShootSound(gameObject);
        var plate = Instantiate(projectile, transform.position, transform.rotation);
        plate.transform.parent = transform;
        currPlates++;
        return plate;
    }

    public void removePlate()
    {
        currPlates--;
    }

    private IEnumerator MovePlate(GameObject plate, Vector3 targetPos)
    {
        while(plate != null && plate.transform.position != targetPos)
        {
            plate.transform.position = Vector3.MoveTowards(plate.transform.position, targetPos, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }

        // plate can be destroyed by the time it reaches its destination if a mouse is in the way
        if (plate != null)
        {
            plate.GetComponentInChildren<SpriteRenderer>().sprite = brokenPlate;
        }
    }
}
