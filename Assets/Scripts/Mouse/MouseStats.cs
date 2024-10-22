using Range;
using ScriptableObjects.Mice;
using UnityEngine;

namespace Mouse
{
    public class MouseStats : MonoBehaviour
    {
        // Stats
        public string mouseName;
        public float speed;
        public float maxHealth;
        public float size;
        public Sprite sprite;
        public bool canGhost;
        public bool canHeal;
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
            maxHealth = mouseStats.health;
            size = mouseStats.size;
            sprite = mouseStats.sprite;
            armoured = mouseStats.armoured;
            ProcessSplitterMouseStats(mouseStats);
            ProcessCanGhost(mouseStats);
            ProcessCanHeal(mouseStats);
        }

        private void ProcessSplitterMouseStats(MiceScriptableObject mouseStats)
        {
            splitMouseType = mouseStats.splitMouseType;
            numOfSplitMice = mouseStats.numOfSplitMice;
            if (CanSplit()) gameObject.AddComponent<MouseSplitter>();

        }

        private void ProcessCanGhost(MiceScriptableObject mouseStats)
        {
            canGhost = mouseStats.canGhost;
            if (canGhost) gameObject.AddComponent<GhostMouse>();
        }

        private void ProcessCanHeal(MiceScriptableObject mouseStats)
        {
            canHeal = mouseStats.canHeal;
            if (canHeal)
            {
                gameObject.AddComponent<MedicMouseRange>();
            }
            else
            {
                gameObject.transform.Find("Pulse").gameObject.SetActive(false);
            }
        }

        void Start()
        {
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite; 
        }

        public bool CanSplit()
        {
            return !string.IsNullOrEmpty(splitMouseType) && numOfSplitMice > 0;
        }
    }
}