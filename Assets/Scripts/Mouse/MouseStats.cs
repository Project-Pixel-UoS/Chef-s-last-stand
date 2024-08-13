using Range;
using UnityEngine.Serialization;

namespace Mouse
{
    using UnityEngine;
    public class MouseStats : MonoBehaviour
    {
        // Stats
        public string mouseName;
        public float speed;
        public float maxHealth;
        public float size;
        public Sprite sprite;
        public bool canGhost;
        public bool canSplit; 
        public bool canHeal;
        public bool armoured;

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
            canSplit = mouseStats.canSplit;
            armoured = mouseStats.armoured;
            
            canGhost = mouseStats.canGhost;
            if (canGhost) gameObject.AddComponent<GhostMouse>();

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
            // Sets sprite according to scritable object
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite; 
        }
    }
}