using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : Projectile
{
    [Header("Technical Variables")]
    [SerializeField] float maxHeight;
    [SerializeField] float tumbleSpeed;

    private float startDistance;
    private float predictedTime;

    private void Start()
    {
        startDistance = Vector3.Distance(targetTransform.position, this.transform.position);
        predictedTime = startDistance / thisRigidbody.velocity.magnitude;

        AddUpwardsVelocity();
        AddTumble();
    }

    private void FixedUpdate()
    {
        if (pointTowardsVelocity) PointTowardsVelocity();
    }

    void AddUpwardsVelocity()
    {
        thisRigidbody.velocity -= Vector3.up * (predictedTime / 2f) * Physics.gravity.y * maxHeight;
    }

    void AddTumble()
    {
        thisRigidbody.AddTorque(new Vector3(Random.Range(-1f,1f) * tumbleSpeed, Random.Range(-1f, 1f) * tumbleSpeed, Random.Range(-1f, 1f) * tumbleSpeed));
    }
}
