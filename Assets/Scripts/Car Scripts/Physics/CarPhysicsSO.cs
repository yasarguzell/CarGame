using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Car Physics", menuName = "ScriptableObjects/CarPhysics")]
public class CarPhysicsSO : ScriptableObject
{
    [Header("Acceleration Settings")]
    public float AccelerationForce;
    public float MaxSpeed;

    [Header("Steering Settings - Drift")]
    public float SteeringConst;
    public AnimationCurve TurnSpeedCurve;
}
