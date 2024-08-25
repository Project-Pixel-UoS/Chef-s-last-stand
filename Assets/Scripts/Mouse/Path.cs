using UnityEngine;

namespace Mouse
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private Transform[] targets;
        

        public Vector2 GetStartPos()
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