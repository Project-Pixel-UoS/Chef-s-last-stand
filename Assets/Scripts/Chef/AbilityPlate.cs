using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chef;
using System.IO;
using Range;
using UnityEngine.Serialization;
using Util;
using System.Linq;

public class AbilityPlate : MonoBehaviour
{
    [FormerlySerializedAs("Projectile")] [SerializeField] private GameObject projectile; // projectile for chef to shoot
    [SerializeField] private float cooldown; // time in between chef shooting (seconds)
    [SerializeField] private float maxPlates;
    [SerializeField] private Sprite brokenPlate;
    private Transform[] turningPoints;
    private Transform[] turningPoints2;
    private float cooldownTimer; // timer for cooldown in between shots
    private float currPlates;
    private ChefRange chefRange;
    private Buff buff;
    
    // Start is called before the first frame update
    void Start()
    {
        var path = GameObject.Find("Path");
        var path2 = GameObject.Find("Path 2");
        turningPoints = path.GetComponentsInChildren<Transform>();
        if(path2 != null ) turningPoints2 = path2.GetComponentsInChildren<Transform>();
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
        AddTPsInRange(tpsInRange, turningPoints);
        if(turningPoints2 != null)
        {
            AddTPsInRange(tpsInRange, turningPoints2);
        }
        return tpsInRange;
    }

    /// <summary>
    /// Add turningpoints  in range from all paths into a list
    /// </summary>
    /// <param name="tpsInRange">list to store turningpoints in range</param>
    /// <param name="tps">the path containing all turningpoints</param>
    private void AddTPsInRange(List<Transform> tpsInRange, Transform[] tps)
    {
        foreach (var turningpoint in tps)
        {
            float distance = (turningpoint.transform.position - transform.position).magnitude;
            if (distance <= chefRange.Radius)
            {
                tpsInRange.Add(turningpoint);
            }
        }
    }
    /// <returns>
    /// a possible plate position on the map
    /// </returns>
    /// <remarks> maintained by: Martin </remarks>
    private Vector3 GetPlatePosition()
    {
        var inRangeTPs = GetTPsInRange();//grab random TP in range
        if (inRangeTPs.Count == 0) return Vector3.back; //if no tp in range, return "no tp code" (-1)
        int rndIndex = Random.Range(0, inRangeTPs.Count); //random tp in range
        Transform[] onThisPath = turningPoints;
        int tpIndex = 0; //index of selected tp in path

        if (turningPoints.Contains(inRangeTPs[rndIndex]))
        {
            tpIndex = System.Array.IndexOf(turningPoints, inRangeTPs[rndIndex]);
        }
        else
        {
            onThisPath = turningPoints2;
            tpIndex = System.Array.IndexOf(turningPoints2, inRangeTPs[rndIndex]);
        }
        
        var adjTPs = onThisPath[(tpIndex-1)..(tpIndex+2)]; //get prev. and next TP of random TP
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
