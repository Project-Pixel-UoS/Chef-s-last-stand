using UnityEngine;

namespace Mouse
{
    public class ProjectileSensor:MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            GameObject weapon = other.gameObject;
            if (!weapon.activeSelf) return;
            weapon.SetActive(false);
            
            if (IsGhostMouse()) return;

            GetComponent<SlownessHandler>().HandleSlownessProjectile(other);
            GetComponent<DamageHandler>().HandleProjectile(other.gameObject);
        }

        private bool IsGhostMouse()
        {
            var ghostMouse = GetComponent<GhostMouse>();
            return ghostMouse != null && !ghostMouse.IsVisible();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsGhostMouse()) return;
            GetComponent<DamageHandler>().HandleProjectile(other.gameObject);
        }


    }
}