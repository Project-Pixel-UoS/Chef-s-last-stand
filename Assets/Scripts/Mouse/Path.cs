using System;
using System.Collections;
using System.Collections.Generic;
using GameManagement;
using Unity.VisualScripting;


using UnityEngine;
using UnityEngine.UI;

namespace Mouse
{
    public class Path : MonoBehaviour
    {
        public Transform[] targets;
        public Transform[] routeSignals;

        private void Start()
        {
            if (routeSignals.Length != 0)
            {
                RouteSignal(routeSignals);
            }
            else
            {
                RouteSignal(targets);
            }
            
        }
        
        /// <summary>
        /// Displays level path at the beginning of the level.
        /// </summary>
        private void RouteSignal(Transform[] tps)
        {
            foreach (Transform point in tps)
            {
                SpriteRenderer sprite = point.GetComponent<SpriteRenderer>();
                IEnumerator c = LevelManager.LM.FadeInAndOut(sprite);
                StartCoroutine(c);
            }
        }

        public Vector3 GetStartPos()
        {
            return targets[0].position;
        }

        /// <summary>
        /// get waypoint transform from path
        /// </summary>
        /// <param name="index">index of waypoint in the path</param>
        /// <returns>the transform object</returns>
        public Transform GetTarget(int index)
        {
            return targets[index];
        }

        public int GetPathLength()
        {
            return targets.Length;
        }
    }
}