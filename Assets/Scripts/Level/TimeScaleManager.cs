using UnityEngine;

namespace Level.WaveData
{
    public class TimeScaleManager : MonoBehaviour
    {

        public static TimeScaleManager instance;
        
        
        private int speedMultiplier = 1;
        public int SpeedMultiplier
        {
            get => speedMultiplier;
            set
            {
                speedMultiplier = value;
                Time.timeScale = value;
            }
        }
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}