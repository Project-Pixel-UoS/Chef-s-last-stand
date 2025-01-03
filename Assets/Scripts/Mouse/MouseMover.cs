using GameManagement;
using Music;

namespace Mouse
{
    using UnityEngine;
    using Cheese = Cheese.Cheese;

    public class MouseMover : MonoBehaviour
    {
        private MouseStats stats;
        private Path path;
        private GameObject path2;
        private Transform target;
        private int targetWayPointIndex = 0;
        public float totalDistanceMoved;
        private float mouseDamage = 10; //The amount of damage that particular mouse causes to the player
        private PlayerHealthManager playerHealth;
        private Cheese cheese;
        public Transform startWayPoint;

        void Awake()
        {
            playerHealth = GameObject.FindGameObjectWithTag("Health").GetComponent<PlayerHealthManager>();
            path = GameObject.Find("Path").GetComponent<Path>();
            path2 = GameObject.Find("Path 2");
            if (transform.parent.name.Equals("MouseFactory2"))
            {
                path = path2.GetComponent<Path>();
            }
            startWayPoint = path.GetTarget(0);
            target = path.GetTarget(targetWayPointIndex);
            stats = gameObject.GetComponent<MouseStats>();
            if (IsCheeseExistent())
            {
                cheese = GameObject.FindGameObjectWithTag("Cheese").GetComponent<Cheese>();
            }

        }


        void Update()
        {
            if (Vector2.Distance(target.position, transform.position) <= 0.1f)
            {
                targetWayPointIndex++;
                if (targetWayPointIndex == path.GetPathLength())
                {
                    ProcessMouseFinish();
                }
                else
                {
                    target = path.GetTarget(targetWayPointIndex);
                    MouseRotate(target);
                }
            }
        }

        private void ProcessMouseFinish()
        {
            Destroy(gameObject);
            SoundPlayer.instance.PlayMousePassedFX();
            if (!GameManager.gameManager.IsGameOver())
            {
                DamagePlayer();
            }

            
        }

        private void DamagePlayer()
        {
            playerHealth.TakeDamage(mouseDamage);
            if (cheese != null && cheese.isActiveAndEnabled)
            {
                cheese.UpdateSpriteIfNecessary();
            }
        }

        private static bool IsCheeseExistent()
        {
            return GameObject.FindGameObjectWithTag("Cheese") != null;
        }

        void FixedUpdate()
        {
            var direction = Vector3.Normalize(target.position - transform.position);
            var movement = direction * stats.speed * Time.deltaTime;
            transform.position += movement;
            totalDistanceMoved += movement.magnitude;
        }

        private void MouseRotate(Transform target)
        {
            Vector3 direction = target.transform.position - transform.position;
            float radians = Mathf.Atan2(direction.x, direction.y) * -1;
            float degrees = radians * Mathf.Rad2Deg;
            Quaternion aim = Quaternion.Euler(0, 0, degrees);
            transform.rotation = aim;
        }

        public void SetTargetWayPointIndex(int i)
        {
            targetWayPointIndex = i;
            target = path.GetTarget(targetWayPointIndex);
            MouseRotate(target);
        }

        /// <summary>
        /// get the next waypoint the mouse is moving to
        /// </summary>
        /// <returns>the waypoint to move to next</returns>
        public Transform GetTargetWayPoint()
        {
            return target;
        }
    }
}