

using Unity.VisualScripting;

namespace Mouse
{
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// Designed to be attached to mice.
    /// Responsible for health of each mice.
    /// </summary>
    /// <remarks>Author: Antosh</remarks>
    public class DamageHandler : MonoBehaviour
    {
        private MouseStats stats;
        [SerializeField] private int currencyAmount;
        [SerializeField] private ParticleSystem onFire;
        private IEnumerator damageCoroutine;
        private GameObject credits;
        private CreditManager creditsManager;
        private SpriteRenderer sprite;
        public IEnumerator flashRedCoroutine = null;
        private MouseHealthHandler mouseHealthHandler;
        

        private void Start()
        {
            stats = gameObject.GetComponent<MouseStats>();
            credits = GameObject.FindGameObjectWithTag("Credits");
            creditsManager = credits.GetComponent<CreditManager>();
            sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
            mouseHealthHandler = gameObject.GetComponent<MouseHealthHandler>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            HandleCollision(other.gameObject);
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            HandleCollision(other.gameObject);
        }


        private void HandleCollision(GameObject weapon)
        {
            StopPoisonousDamage();
            StartDamageCoroutine(weapon);
        }

        private void StartDamageCoroutine(GameObject weapon)
        {
            DamageFactor damageFactor = HandleArmouredMouse(weapon);
            damageCoroutine = TakeDamage(damageFactor);
            StartCoroutine(damageCoroutine);
        }

        private void StopPoisonousDamage()
        {
            if (damageCoroutine != null) //check mouse still taking poisonous damage
            {
                StopCoroutine(damageCoroutine);
            }
        }

        /// <summary>
        /// Deal damage, instantaneously or over time if in bound projectile has long lasting effects
        /// </summary>
        /// <param name="damageFactor">container for projectile's damage stats</param>
        /// <remarks>Author: Antosh</remarks>
        public IEnumerator TakeDamage(DamageFactor damageFactor)
        {
            float durationRemaining = damageFactor.damageDuration;
            if (IsBurning(damageFactor)) onFire.Play();
            
            // HandleBurnChain(damageFactor); - functionality temporarily disabled due to bugs
            while (durationRemaining > 0) //take damage until long lasting effect runs out
            {
                mouseHealthHandler.DecrementHealth(damageFactor.damage);
                durationRemaining -= damageFactor.damageRate;
                
                if (mouseHealthHandler.Health <= 0)
                {
                    HandleMouseSplit();
                    creditsManager.IncreaseMoney(currencyAmount); //get money per kill
                    try
                    {
                        Destroy(gameObject);
                        break;
                    }
                    catch
                    {
                        break;
                    }
                }

                if (damageFactor.damage != 0)
                {
                    flashRedCoroutine = FlashRed();
                    StartCoroutine(flashRedCoroutine);
                }
                yield return new WaitForSeconds(damageFactor.damageRate);
                if (this == null) yield break; // check object is destroyed
            }

            if (onFire != null) onFire.Stop();
            
        }

        /// <summary>
        /// Make mouse flash red when damage is delt.
        /// </summary>
        /// <remarks>Author: Emily</remarks>
        public IEnumerator FlashRed()
        {
            sprite.color = UnityEngine.Color.red;
            yield return new WaitForSeconds(0.1f);
            sprite.color = UnityEngine.Color.white;
            flashRedCoroutine = null;
        }

        //if the mouse that died was trench coat, grab its death position and spawn more mice.
        private void HandleMouseSplit()
        {
            if (stats.CanSplit())
            {
                GetComponent<MouseSplitter>().Split(stats.numOfSplitMice, stats.splitMouseType, transform.GetComponent<MouseMover>());
            }
       
        }

        /// <summary>
        /// if an armoured mouse is hit by onions or knives, do no damage.
        /// </summary>
        /// <param name="projectile">the collision object it collided with</param>
        /// <returns></returns>
        /// TODO Should not need this method, should just return from method, TEST LATER
        private DamageFactor HandleArmouredMouse(GameObject projectile)
        {
            var damageFactor = projectile.GetComponent<DamageFactor>();
            if (stats.armoured && projectile.GetComponent<SlownessProjectile>() == null)
            {
                damageFactor.damage = 0;
                damageFactor.damageRate = 1;
                damageFactor.damageDuration = 1;
            }
            return damageFactor;
        }

        /// <summary>
        /// checks if the grillardin 3/4 is damaging the mouse.
        /// </summary>
        /// <param name="damageFactor">the chef's damage script</param>
        /// <returns>true if grillardin 3/4 is damaging the mouse, false otherwise.</returns>
        private bool IsBurning(DamageFactor damageFactor)
        {
            string currChef = damageFactor.chef.name;
            return currChef.Equals("Chef Grillardin 3(Clone)") || currChef.Equals("Chef Grillardin 4(Clone)");
        }

        /// <summary>
        /// whether the burning animation is playing.
        /// </summary>
        /// <returns>true if animation is playing, false otherwise.</returns>
        public bool IsBurning()
        {
            return onFire.isPlaying;
        }

        /// <summary>
        /// For level 4 grillardin upgrade. When mouse is burning, grab surrounding mice and deal damage to them too.
        /// </summary>
        /// <param name="damageFactor">the grillardin's damage script</param>
        private void HandleBurnChain(DamageFactor damageFactor)
        {
            if (damageFactor.chef.name.Equals("Chef Grillardin 4(Clone)"))
            {
                var colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
                if (colliders != null)
                {
                    foreach (Collider2D collider in colliders)
                    {
                        GameObject mouse = collider.gameObject;
                        if (mouse.CompareTag("Mouse") && mouse != gameObject)
                        {
                            mouse.GetComponent<DamageHandler>().TakeDamage(damageFactor);
                        }
                    }
                }
            }
        }
    }
}