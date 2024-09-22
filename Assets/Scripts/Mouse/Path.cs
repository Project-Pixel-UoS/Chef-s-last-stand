using System;
using System.Collections;
using System.Collections.Generic;
using GameManagement;
using Unity.VisualScripting;


using UnityEngine;

namespace Mouse
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private Transform[] targets;

        private void Start()
        {
            
            RouteSignal(targets);
        }
        
        /// <summary>
        /// Displays level path at the beginning of the level.
        /// </summary>
        private void RouteSignal(Transform[] tps)
        {
            foreach (Transform point in tps)
            {
                SpriteRenderer sprite = point.GetComponent<SpriteRenderer>();
                IEnumerator c = LevelManager.LM.FadeIn(sprite);
                print("c: " + c);
                StartCoroutine(c);
            }
        }

        public Vector3 GetStartPos()
        {
            return targets[0].position;
        }

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