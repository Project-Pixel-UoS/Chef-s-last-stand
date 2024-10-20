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
        public GameObject SpawnMouse(MiceScriptableObject mouseType, Vector3 position, int wayPointIndex, float pathDistance)
        {
            GameObject newMouse;
            var startPos = GameObject.Find("Path").GetComponent<Path>().GetStartPos();
            if (position == startPos && pathDistance == 0)
            {
                newMouse = Instantiate(mousePrefab, position, transform.rotation, transform);
            }
            else
            {
                GameObject factory = GameObject.Find("MouseFactory2");
                newMouse = Instantiate(mousePrefab, position, transform.rotation, factory.transform);
            }
             // Instantiate mouse prefab
            newMouse.GetComponent<MouseStats>().loadStats(mouseType);
            var spriteMove = newMouse.GetComponent<MouseMover>();
            spriteMove.SetTargetWayPointIndex(wayPointIndex);
            spriteMove.totalDistanceMoved = pathDistance; // only needs to be modified when 
            Util.Utils.ResizeSpriteOutsideCanvas(newMouse);
            return newMouse;
        }

        //overloaded function for splitting mice
        public GameObject SpawnMouse(string mouseType, Vector3 position, int wayPointIndex, float pathDistance, GameObject factory)
        {
            var newMouse = Instantiate(mousePrefab, position, transform.rotation, factory.transform);
            newMouse.GetComponent<MouseStats>().loadStats(GetMouseType(mouseType));
            var spriteMove = newMouse.GetComponent<MouseMover>();
            spriteMove.SetTargetWayPointIndex(wayPointIndex);
            spriteMove.totalDistanceMoved = pathDistance; // only needs to be modified when 
            Util.Utils.ResizeSpriteOutsideCanvas(newMouse);
            return newMouse;
        }

            /// <summary>Spawns a mouse of indicated mouse type at specified position.</summary>
        public GameObject SpawnMouse(string mouseType, Vector3 position, int wayPointIndex, float pathDistance)
        {
            return SpawnMouse(GetMouseType(mouseType), position, wayPointIndex, pathDistance);
        }

        
        /// <summary>Spawns mouse at the start</summary>
        /// <param name="mouseType"> type of mouse that will be spawned in</param>
        public void SpawnMouse(MiceScriptableObject mouseType)
        {
            var startPos = GameObject.Find("Path").GetComponent<Path>().GetStartPos();
            SpawnMouse(mouseType, startPos,1, 0);

            var startPos2 = GameObject.Find("Path 2");
            if(startPos2 != null)
            {
                SpawnMouse(mouseType, startPos2.GetComponent<Path>().GetStartPos(), 1, 0);
            }
            
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