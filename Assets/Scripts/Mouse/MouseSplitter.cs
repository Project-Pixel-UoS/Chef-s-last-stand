using System;
using System.Linq;
using UnityEngine;

namespace Mouse
{
    public class MouseSplitter : MonoBehaviour
    {
        public void Split(int numOfMice, string mouseType, MouseMover parent)
        {
            float totalDistanceMoved = GetComponent<MouseMover>().totalDistanceMoved;
            for (int i = 0; i < numOfMice; i++)
            {
                float spawnDistance = totalDistanceMoved - i;
                var wayPoints = GameObject.Find("Path").GetComponent<Path>().targets;
                var wayPoints2 = GameObject.Find("Path 2");
                var factory = GameObject.Find("MouseFactory");
                var factory2 = GameObject.Find("MouseFactory2");
                //if the parent started at path2
                if (wayPoints2 != null && transform.parent == factory2.transform){
                    print("spawned path2");
                    (Vector2 spawnPos, int targetWayPointIndex) = ConvertDistanceToPos(spawnDistance, wayPoints2.GetComponent<Path>().targets);
                    MouseFactory.Instance.SpawnMouse(mouseType, spawnPos, targetWayPointIndex, spawnDistance, factory2);
                }
                else
                {
                    (Vector2 spawnPos, int targetWayPointIndex) = ConvertDistanceToPos(spawnDistance, wayPoints);
                    MouseFactory.Instance.SpawnMouse(mouseType, spawnPos, targetWayPointIndex, spawnDistance, factory);
                }
            }
        }

        /// <param name="mouseDistanceTravelled"> total amount mouse have travelled since spawning</param>
        /// <returns>position of mouse spawn point, target waypoint index, target way point</returns>
        /// <exception cref="ArgumentException"> distanceTravelled input is larger then the maximum</exception>
        private (Vector2, int) ConvertDistanceToPos(float mouseDistanceTravelled, Transform[] wayPoints)
        {
            for (int i = 1; i < wayPoints.Length; i++)
            {
                // distance between 2 way points
                float separation = (wayPoints[i].position - wayPoints[i - 1].position).magnitude;
                if (mouseDistanceTravelled > separation)
                {
                    mouseDistanceTravelled -= separation; // mouse distance travelled from waypoint i
                }
                else
                {
                    var next = wayPoints[i].position; // position of next waypoint mouse was aiming for
                    var previous = wayPoints[i - 1].position; // position of waypoint mouse most recent passed
                    var posOfDeath = Vector2.Lerp(previous, next, 
                        mouseDistanceTravelled / separation);
                    return (posOfDeath, i);
                }
            }
            throw new ArgumentException("distance input greater than max possible distance of route");
        }
    }
}