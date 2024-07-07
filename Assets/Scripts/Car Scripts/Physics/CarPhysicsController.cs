using UnityEngine;

namespace CarGame.CarPhysics
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarPhysicsController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private FixedJoystick _joystick;

        [Header("Suspension Settings")]
        public float SpringRestPosition;
        public float SpringConstant;
        public float DampingConstant;

        [Header("Steering Settings")]
        [Range(0, 1)]
        public float TireGrip;
        public float TireMass;

        [Header("Acceleration Settings")]
        public float CarTopSpeed;
        public float ForwardForce;

        private Rigidbody _rb;
        private Transform _transform;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _transform = transform;
        }

        private void FixedUpdate()
        {
            float horizontalInput = _joystick.Horizontal;
            float verticalInput = _joystick.Vertical;
            Vector2 direction = new Vector2(verticalInput, horizontalInput);
            if (direction.magnitude < Mathf.Epsilon)
                return;
                
            direction.Normalize();

            Steering(direction);
        }

        private void Steering(Vector2 direction)
        {
            float angleInRadians = Mathf.Atan2(direction.y, direction.x);
            float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
            Vector3 currentEulerAngles = _rb.rotation.eulerAngles;
            Quaternion targetRotation = Quaternion.Lerp(_transform.rotation, Quaternion.Euler(currentEulerAngles.x, angleInDegrees, currentEulerAngles.z), 0.03f);
            _rb.MoveRotation(targetRotation);
        }
    }
}