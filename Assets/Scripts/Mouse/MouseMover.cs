using GameManagement;

namespace Mouse
{
    using UnityEngine;
    using Cheese = Cheese.Cheese;

    public class MouseMover : MonoBehaviour
    {
        private MouseStats stats;
        private Path path;
        private Transform target;
        private int targetWayPointIndex = 0;
        public float totalDistanceMoved;
        private float mouseDamage = 10; //The amount of damage that particular mouse causes to the player
        private PlayerHealthManager playerHealth;
        private Cheese cheese;


        void Awake()
        {
            cheese = GameObject.FindGameObjectWithTag("Cheese").GetComponent<Cheese>();
            playerHealth = GameObject.FindGameObjectWithTag("Health").GetComponent<PlayerHealthManager>();
            stats = gameObject.GetComponent<MouseStats>();
            // path.targets = LevelManager.LM.TurningPoints;
            target = path.targets[targetWayPointIndex];
        }



        void Update()
        {
            if (Vector2.Distance(target.position, transform.position) <= 0.1f)
            {
                targetWayPointIndex++;
                if (targetWayPointIndex == path.targets.Length)
                {
                    ProcessMouseFinish();
                }
                else
                {
                    MouseRotate(path.targets[targetWayPointIndex]);
                    target = path.targets[targetWayPointIndex];
                }
            }
        }

        private void ProcessMouseFinish()
        {
            Destroy(gameObject);
            if (!GameManager.gameManager.IsGameOver())
            {
                DamagePlayer();
            }
        }

        private void DamagePlayer()
        {
            playerHealth.TakeDamage(mouseDamage);
            if (IsCheeseExistent())
            {
                cheese.GetComponent<Cheese>().UpdateSpriteIfNecessary();
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
            target = path.targets[targetWayPointIndex];
            MouseRotate(target);
        }
    }
}