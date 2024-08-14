using System.Collections;
using UnityEngine;

public class SlownessProjectile : MonoBehaviour
{
    [SerializeField] public float slownessFactor = 5; //how much the mouse will be slowed down by
    [SerializeField] public float duration = 3; //how long the mouse will be slowed down for
    public float projectileSpeed; // speed of projectile
    public bool hit = false;
    [SerializeField] private float destroyTime; // time until projectile is destroyed (seconds)
    [SerializeField] private GameObject soup;

    void Update()
    {
        if (!hit) transform.position += transform.up * Time.deltaTime * projectileSpeed;      // update position

        // destroy game object when timer reaches zero
        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    // if projectile collides with game object, it is destroyed
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mouse"))
        {
            hit = true;
            if (transform.name.Equals("Projectile Potager 4(Clone)"))
            {
                //StartCoroutine(GoopDelay(duration, collision));
                GameObject gooper = Instantiate(soup, collision.transform.position, collision.transform.rotation);
                Debug.Log(gooper.name, gooper);
                Destroy(gooper, duration);
            }
            Destroy(gameObject);
        }
    }

}
