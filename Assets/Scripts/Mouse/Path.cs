using System;
using GameManagement;
using Unity.VisualScripting;
using UnityEngine;

namespace Mouse
{
    public class Path : MonoBehaviour
    {
        public Transform[] targets;
        
        // once cheese has been eaten, need a new destination co ordinate.
        public Vector2 mouseEdgeEndpointPos = new Vector2(3.4f, -4.7f);
        

        public void Start()
        {
            if (GameObject.FindGameObjectWithTag("Cheese") != null)
            {
                GameManager.onGameOver += MoveMouseDestinationToScreenEdge;
            }
        }
        
        private void MoveMouseDestinationToScreenEdge()
        {
            targets[targets.Length - 1].position = mouseEdgeEndpointPos;
        }

        public Vector2 StartPos()
        {
            return targets[0].position;
        }
    }
}