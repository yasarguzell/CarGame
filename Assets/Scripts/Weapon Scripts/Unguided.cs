using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unguided : Projectile
{
    private void FixedUpdate()
    {

        if (pointTowardsVelocity) PointTowardsVelocity();
    }
}
