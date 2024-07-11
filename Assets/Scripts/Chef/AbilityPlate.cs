using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chef;
using System.IO;
using Util;

public class AbilityPlate : MonoBehaviour
{
    [SerializeField] private GameObject Projectile; // projectile for chef to shoot
    [SerializeField] private float cooldown; // time in between chef shooting (seconds)
    [SerializeField] private float maxPlates;
    private Transform[] turningPoints;
    private float cooldownTimer; // timer for cooldown in between shots
    private float currPlates;
    private Range range;
    // Start is called before the first frame update
    void Start()
    {
        var path = GameObject.Find("Path");
        turningPoints = path.GetComponentsInChildren<Transform>();
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
            if (distance <= range.Radius)
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
            if ((randomPos - transform.position).magnitude < range.Radius)
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
        var plate = Instantiate(Projectile, transform.position, transform.rotation);
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
    }
}
