using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningProjectile : MonoBehaviour
{
    [SerializeField] GameObject visual;
    [SerializeField] float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        visual.transform.Rotate(0,0,rotationSpeed*Time.deltaTime);
    }
}
