using UnityEngine.Serialization;

namespace Mouse
{
    using UnityEngine;

    public class MouseStats : MonoBehaviour
    {
        // Stats
        public string mouseName;
        public float speed;
        public float health;
        public float size;
        public Sprite sprite;
        public bool canGhost;
        public bool armoured;

        public string splitMouseType = ""; // mouse type that is produced upon death. Empty string if none 
        public int numOfSplitMice; // amount of mice produced upon death. Set to 0 if none


        /// <summary> Puts stats into relevant variables from a given ScriptableObject </summary>
        /// <param name = "mouseStats"> MiceScriptableObject containing stats for that mouse type </param>
        /// <remarks>Maintained by: Ben Brixton </remarks>
        public void loadStats(MiceScriptableObject mouseStats)
        {
            mouseName = mouseStats.mouseName;
            speed = mouseStats.speed;
            health = mouseStats.health;
            size = mouseStats.size;
            sprite = mouseStats.sprite;
            canGhost = mouseStats.canGhost;
            if (canGhost) gameObject.AddComponent<GhostMouse>();
            armoured = mouseStats.armoured;

            splitMouseType = mouseStats.splitMouseType;
            numOfSplitMice = mouseStats.numOfSplitMice;
            if (CanSplit()) gameObject.AddComponent<MouseSplitter>();
        }

        void Start()
        {
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =
                sprite; // Sets sprite according to scritable object
        }

        public bool CanSplit()
        {
            return !string.IsNullOrEmpty(splitMouseType) && numOfSplitMice > 0;
        }
    }
}