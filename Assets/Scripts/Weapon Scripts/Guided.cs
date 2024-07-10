using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guided : Projectile
{
    [Header("Technical Variables")]
    [SerializeField] float acceleration;
    [SerializeField] float angleChangePerSecond;
    [SerializeField] float maxMobilityVelocity;

    Vector3 targetPosition;

    // Update is called once per frame
    void FixedUpdate()
    {
        VelocityIncrement();
        GuidanceIncrement();
        if (pointTowardsVelocity) PointTowardsVelocity();
    }

    void VelocityIncrement()
    {
        thisRigidbody.velocity += thisRigidbody.velocity.normalized * acceleration * Time.fixedDeltaTime;
    }

    void GuidanceIncrement()
    {
        if (!targetTransform)
        {
            Debug.Log("aaaaa");
            return;
        }

        targetPosition = targetTransform.position;

        Vector3 velocityDirection = thisRigidbody.velocity.normalized;
        Vector3 guidanceTargetDirection = (targetPosition - this.transform.position).normalized;
        Vector3 normalVector = Vector3.Cross(velocityDirection, guidanceTargetDirection);

        float increment = (angleChangePerSecond * Mathf.Clamp((thisRigidbody.velocity.magnitude / maxMobilityVelocity), 0.0f, 1.0f) * Time.fixedDeltaTime);
        if (Vector3.Angle(velocityDirection, guidanceTargetDirection) > increment)
        {
            thisRigidbody.velocity = Quaternion.AngleAxis(increment, normalVector) * velocityDirection * thisRigidbody.velocity.magnitude;
        }
        else
        {
            thisRigidbody.velocity = this.thisRigidbody.velocity.magnitude * guidanceTargetDirection;
        }
    }
}
