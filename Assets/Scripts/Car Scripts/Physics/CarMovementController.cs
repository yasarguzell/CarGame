using UnityEngine;

public class CarMovementController : MonoBehaviour
{
    [Header("Car Settings SO")]
    [SerializeField] private CarPhysicsSO _carPhysics;
    [Header("Current Car Speed - Readonly")]
    [SerializeField] private float _velocity;

    [Header("Acceleration Settings")]
    public float _accelerationForce;
    public float _maxSpeed;

    [Header("Steering Settings - Drift")]
    [SerializeField] private float _steeringConst;
    [SerializeField] private AnimationCurve TurnSpeedCurve;

    [Header("References")]
    [SerializeField] private FixedJoystick Joystick;
    [SerializeField] private Transform[] _rearWheels;
    [SerializeField] private Transform[] _frontWheels;

    private Vector2 _input;
    private float _inputAngleInDegrees;
    private bool _isGrounded;

    private Rigidbody _rb;
    private Transform _transform;

    // Wheel mesh rotation values for visual
    private float _maxSteerAngle = 15f; // Rotates wheel meshes for visual 
    private Direction _steeringDirection = Direction.frw;
    private float _wheelRadius = 0.5f;


    private Transform[] _wheelTransforms;


    private enum Direction
    {
        left,
        right,
        frw
    }

    private void Awake()
    {
        _transform = transform;
        _rb = GetComponent<Rigidbody>();
        GetScriptableObjectSettings();
        _wheelTransforms = new Transform[] { _frontWheels[0], _frontWheels[1], _rearWheels[0], _rearWheels[1] };
    }

    private void Update()
    {
        GetInputAccordingToCameraAngle(out _input);
        CalculateInputAngle(out _inputAngleInDegrees);

        RotateWheelMeshes();
        ApplySteeringRotationToWheelMeshes();

        CheckIsGrounded(out _isGrounded);

        StopRotationOnZAxis();
    }

    void FixedUpdate()
    {
        _velocity = _rb.velocity.magnitude;

        _rb.drag = _isGrounded ? 1 : 0;

        if (!_isGrounded || Mathf.Abs(_input.magnitude) < Mathf.Epsilon)
            return;

        Acceleration();
        Steering();
    }

    private void StopRotationOnZAxis()
    {
        _rb.angularVelocity = new Vector3(_rb.angularVelocity.x, _rb.angularVelocity.y, 0);
        if (Mathf.Abs(_transform.rotation.eulerAngles.z) > 0)
        {
            Quaternion quaternion = new Quaternion
            {
                eulerAngles = new Vector3(_transform.rotation.eulerAngles.x, _transform.rotation.eulerAngles.y, 0)
            };
            _rb.MoveRotation(quaternion);
        }
    }

    private void Acceleration()
    {
        var force = _velocity < _maxSpeed ? _transform.forward * _accelerationForce * _input.magnitude : Vector3.zero;
        _rb.AddForce(force);
    }


    private void Steering()
    {
        float angleInRadians = Mathf.Atan2(_input.y, _input.x);
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
        Vector3 currentEulerAngles = _rb.rotation.eulerAngles;
        float lerpValue = TurnSpeedCurve.Evaluate(_rb.velocity.magnitude) * _steeringConst;
        Quaternion targetRotation = Quaternion.Lerp(_transform.rotation, Quaternion.Euler(currentEulerAngles.x, angleInDegrees, currentEulerAngles.z), lerpValue);
        _rb.MoveRotation(targetRotation);
    }

    private void CheckIsGrounded(out bool isGrounded)
    {
        isGrounded = Physics.Raycast(_transform.position + _transform.up * 0.5f, -_transform.up, 1f);
        if (isGrounded)
            Debug.DrawRay(_transform.position + _transform.up * 0.5f, -_transform.up, Color.green, 1f);
        else
            Debug.DrawRay(_transform.position + _transform.up * 0.5f, -_transform.up, Color.red, 1f);
    }

    void ApplySteeringRotationToWheelMeshes()
    {
        float currentYRotation = transform.eulerAngles.y;
        float rotationDelta = Mathf.Abs(_input.magnitude) < Mathf.Epsilon ? 0 : Mathf.DeltaAngle(_inputAngleInDegrees, currentYRotation);

        float steerAngle = 0f;

        if (rotationDelta > 1f)
        {
            if (_steeringDirection == Direction.left)
                return;
            steerAngle = -_maxSteerAngle;
            _steeringDirection = Direction.left;
        }
        else if (rotationDelta < -1f)
        {
            if (_steeringDirection == Direction.right)
                return;
            steerAngle = _maxSteerAngle;
            _steeringDirection = Direction.right;
        }
        else
        {
            if (_steeringDirection == Direction.frw)
                return;
            _steeringDirection = Direction.frw;
        }

        foreach (Transform frontWheel in _frontWheels)
        {
            Vector3 localEulerAngles = frontWheel.localEulerAngles;
            localEulerAngles.y = steerAngle;
            frontWheel.localEulerAngles = localEulerAngles;
        }
    }

    private void RotateWheelMeshes()
    {
        float carSpeed = _rb.velocity.magnitude;

        float wheelAngularVelocity = carSpeed / _wheelRadius;

        float rotationDegrees = wheelAngularVelocity * Mathf.Rad2Deg * Time.deltaTime;
        foreach (Transform wheelTransform in _wheelTransforms)
        {
            wheelTransform.Rotate(Vector3.right, rotationDegrees);
        }
    }

    private void CalculateInputAngle(out float degree)
    {
        float inputAngleInRadians = Mathf.Atan2(_input.y, _input.x);
        degree = inputAngleInRadians * Mathf.Rad2Deg;
    }

    private void GetInputAccordingToCameraAngle(out Vector2 input)
    {
        float horizontalInput = Joystick.Horizontal;
        float verticalInput = Joystick.Vertical;
        float facing = Camera.main.transform.eulerAngles.y;
        Vector3 myInput = new Vector3(verticalInput, horizontalInput, 0);
        Vector3 myTurnedInputs = Quaternion.Euler(0, 0, facing) * myInput;
        input = new Vector2(myTurnedInputs.x, myTurnedInputs.y);
    }

    public void GetScriptableObjectSettings()
    {
        _accelerationForce = _carPhysics.AccelerationForce;
        _maxSpeed = _carPhysics.MaxSpeed;
        _steeringConst = _carPhysics.SteeringConst;
        TurnSpeedCurve = _carPhysics.TurnSpeedCurve;
    }
}
