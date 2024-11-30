using System.Collections;
using Mouse;
using UnityEngine;
using UnityEngine.Serialization;

public class SlownessProjectile : MonoBehaviour
{
    [SerializeField] public float slownessFactor; //how much the mouse will be slowed down by
    [SerializeField] public float duration = 3; //how long the mouse will be slowed down for
    [SerializeField] public GameObject gooper;


}
