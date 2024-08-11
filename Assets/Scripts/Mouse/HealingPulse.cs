using UnityEngine;

namespace Mouse
{
    public class HealingPulse : MonoBehaviour
    {
        private float range;
        private float maxRange = 1 ;

        private void Update()
        {
            range += 0.005f;
            if (range > maxRange)
            {
                range = 0;
            }
            transform.localScale = new Vector3(range, range);
        }
    }
}