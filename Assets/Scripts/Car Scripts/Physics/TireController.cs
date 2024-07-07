using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame.CarPhysics
{
    public class TireController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CarPhysicsController _carPhysicsController;
        [SerializeField] private FixedJoystick _joystick;

        [Header("Settings")]
        [SerializeField] private bool _isFrontWheel;

        private Rigidbody _carRb;
        private float _springRestPosition;
        private float _springConstant;
        private float _dampingConstant;
        private float _tireGrip;
        private float _originalTireGrip;
        private float _tireMass;
        private float _carTopSpeed;
        private float _forwardForce;

        private Transform _transform;

        private void Awake()
        {
            _carRb = _carPhysicsController.GetComponent<Rigidbody>();
            SetSuspensionValues();
            _transform = transform;
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Space))
                _tireGrip = 0;
            else
                _tireGrip = _originalTireGrip;

            RaycastHit hit;
            if (!Physics.Raycast(transform.position, -_transform.up, out hit, _springRestPosition))
                return;

            float horizontalInput = _joystick.Horizontal;
            float verticalInput = _joystick.Vertical;
            Vector2 direction = new Vector2(verticalInput, horizontalInput);
            direction.Normalize();

            Vector3 tireVelocity = _carRb.GetPointVelocity(_transform.position);

            float inputForce = direction.magnitude;

            if (inputForce < Mathf.Epsilon)
            {
                _carRb.drag = 2.5f;
            }
            else
            {
                _carRb.drag = 1;
            }

            Suspension(tireVelocity, hit.distance);
            Steering(tireVelocity);
            AccelAndBrake(inputForce);
        }

        private void Steering(Vector3 tireVelocity)
        {
            Vector3 steeringDir = _transform.right;
            float steeringVel = Vector3.Dot(steeringDir, tireVelocity);
            float desiredVel = -steeringVel * _tireGrip;
            float desiredAccel = desiredVel / Time.fixedDeltaTime;
            _carRb.AddForceAtPosition(steeringDir * desiredAccel * _tireMass, _transform.position);
        }

        private void Suspension(Vector3 tireVelocity, float hitDistance)
        {
            Vector3 springForceDir = _transform.up;
            float velocityInDamperDir = Vector3.Dot(springForceDir, tireVelocity);
            float offset = _springRestPosition - hitDistance;
            float force = (offset * _springConstant) - (velocityInDamperDir * _dampingConstant);
            _carRb.AddForceAtPosition(springForceDir * force, _transform.position);
        }

        private void AccelAndBrake(float accelInput)
        {
            Vector3 accelDir = _transform.forward;

            if (accelInput > 0.0f)
            {
                float carSpeed = Vector3.Dot(_transform.forward, _carRb.velocity);
                float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / _carTopSpeed);
                float torque = normalizedSpeed <= 1 ? accelInput * 0.6f : 0;
                _carRb.AddForceAtPosition(accelDir * torque * _forwardForce, _transform.position);
            }

        }

        [ContextMenu("Set Suspension Values")]
        private void SetSuspensionValues()
        {
            _springRestPosition = _carPhysicsController.SpringRestPosition;
            _springConstant = _carPhysicsController.SpringConstant;
            _dampingConstant = _carPhysicsController.DampingConstant;
            _tireGrip = _carPhysicsController.TireGrip;
            _originalTireGrip = _tireGrip;
            _tireMass = _carPhysicsController.TireMass;
            _carTopSpeed = _carPhysicsController.CarTopSpeed;
            _forwardForce = _carPhysicsController.ForwardForce;
        }
    }
}