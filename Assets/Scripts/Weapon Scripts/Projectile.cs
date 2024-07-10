using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile: MonoBehaviour
{
    [HideInInspector]
    public Transform targetTransform;
    [HideInInspector]
    public Transform spawnPointTransform;
    [HideInInspector]
    public Rigidbody thisRigidbody;

    public bool pointTowardsVelocity;
    public float damage;


    private void Start()
    {
        thisRigidbody = this.transform.GetComponent<Rigidbody>();
    }

    public void PointTowardsVelocity()
    {
        Vector3 velocityDirection = thisRigidbody.velocity.normalized;
        this.transform.up = velocityDirection;
    }
}
