using System;
using System.Collections.Generic;
using Level.WaveData.WaveData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mouse
{
    /// <summary>
    /// Responsible for spawning mice, to be attached to the Mouse Factory game object
    /// </summary>
    /// <remarks>Author: Antosh</remarks>
    public class MouseFactory : MonoBehaviour
    {
        [SerializeField] private GameObject mousePrefab;
        public MiceScriptableObject[] mouseTypesList;
        
        public static MouseFactory Instance { get; private set; }

        private void Awake()
        {
            //singleton pattern
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        
        /// <summary>Spawns a mouse of indicated mouse type at specified position.</summary>
        public void SpawnMouse(MiceScriptableObject mouseType, Vector3 position, int wayPointIndex, float pathDistance)
        {
            GameObject newMouse = Instantiate(mousePrefab, position, transform.rotation); // Instantiate mouse prefab
            newMouse.GetComponent<MouseStats>().loadStats(mouseType);
            var spriteMove = newMouse.GetComponent<MouseMover>();
            spriteMove.SetTargetWayPointIndex(wayPointIndex);
            spriteMove.totalDistanceMoved = pathDistance; // only needs to be modified when 
        }
        
        /// <summary>Spawns a mouse of indicated mouse type at specified position.</summary>
        public void SpawnMouse(string mouseType, Vector3 position, int wayPointIndex, float pathDistance)
        {
            SpawnMouse(GetMouseType(mouseType), position, wayPointIndex, pathDistance);
        }

        
        /// <summary>Spawns mouse at the start</summary>
        /// <param name="mouseType"> type of mouse that will be spawned in</param>
        public void SpawnMouse(MiceScriptableObject mouseType)
        {
            SpawnMouse(mouseType, GameObject.FindGameObjectWithTag("Path").gameObject.GetStartPos,1, 0);
        }
        
        
        /// <summary>Spawns a mouse of indicated mouse type at specified position.</summary>
        public void SpawnMouse(string mouseType)
        {
            SpawnMouse(GetMouseType(mouseType));
        }

        /// <summary>Spawns mouse at the start</summary>
        public void SpawnMouse(MouseDifficulty difficulty)
        {
            SpawnMouse(GetRandomMouseType(difficulty));
        }
        
        private MiceScriptableObject GetRandomMouseType(MouseDifficulty mouseUnitDifficulty)
        {
            List<MiceScriptableObject> sameDifficultyMice = GetAllMouseTypes(mouseUnitDifficulty);
            return sameDifficultyMice[Random.Range(0, sameDifficultyMice.Count)];
        }

        /// <returns> MiceScriptableObject that corresponds to the mouseName provided</returns>
        /// <exception cref="ArgumentException"> thrown if mouseName does not correspond to any type of mouse</exception>
        /// <remarks>Maintained by Antosh</remarks>
        private MiceScriptableObject GetMouseType(string mouseName)
        {
            foreach (var mouseType in mouseTypesList)
            {
                if (mouseType.mouseName == mouseName)
                {
                    return mouseType;
                }
            }
            throw new ArgumentException("The mouse that your provided doesnt exist!");
        }
        
    
        /// <returns>list of all the mouse types corresponding to the mouse unity difficulty</returns>
        private List<MiceScriptableObject> GetAllMouseTypes(MouseDifficulty mouseUnitDifficulty)
        {
            List<MiceScriptableObject> sameDifficultyMice = new List<MiceScriptableObject>();
            foreach (var mouseType in mouseTypesList)
            {
                if (mouseType.difficulty == mouseUnitDifficulty)
                {
                    sameDifficultyMice.Add(mouseType);
                }
            }
            return sameDifficultyMice;
        }

    }
}