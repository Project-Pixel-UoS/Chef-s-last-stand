using UnityEngine;

namespace Projectile
{
    public class SlownessProjectile : MonoBehaviour
    {
        [SerializeField] public float slownessFactor = 5; //how much the mouse will be slowed down by
        [SerializeField] public float duration = 3; //how long the mouse will be slowed down for

    }
}
