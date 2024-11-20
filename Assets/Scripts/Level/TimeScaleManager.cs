using UnityEngine;

namespace Level.WaveData
{
    public static class TimeScaleManager
    {
        private static int speedMultiplier = 1;
        public static int SpeedMultiplier
        {
            get => speedMultiplier;
            set
            {
                speedMultiplier = value;
                Time.timeScale = value;
            }
        }
    }
}