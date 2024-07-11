using CarGame.Car;
using UnityEngine;

public class ExhaustParticleController : MonoBehaviour
{
    [SerializeField] private CarMovementController movementController;
    [SerializeField] private ParticleSystem[] exhaustParticleSystems;
    [SerializeField] private float sizeAcceleration;
    [SerializeField] private float lifeAcceleration;
    [SerializeField] private float sizeVelocity;
    [SerializeField] private float lifeVelocity;
    [SerializeField] private Rigidbody carRigidbody;

    private ParticleSystem.MainModule[] mainModules;

    private float maxAccel = 0.75f;
    private float maxSpeed = 100f;
    private int counter;

    private float prvVelocity;

    void Start()
    {
        mainModules = new ParticleSystem.MainModule[exhaustParticleSystems.Length];
        for (int i = 0; i < exhaustParticleSystems.Length; i++)
        {
            if (exhaustParticleSystems[i] != null)
            {
                mainModules[i] = exhaustParticleSystems[i].main;
            }
        }
    }

    void FixedUpdate()
    {
        counter++;
        if (counter % 10 == 0)
            maxSpeed = movementController.MaxSpeed;
            
        var velocity = carRigidbody.velocity.magnitude;
        var velocityPrecentage = velocity / maxSpeed;

        var acceleration = Mathf.Abs(velocity - prvVelocity);
        var absAccPrecantage = Mathf.Abs(acceleration / maxAccel);

        prvVelocity = velocity;

        ShawVehicleEffort(velocityPrecentage, absAccPrecantage);
    }

    private void ShawVehicleEffort(float vMod, float aMod)
    {
        if (aMod > 0.2)
        {
            for (int i = 0; i < 2; i++)
            {
                mainModules[i].startSize = sizeAcceleration * aMod;
                mainModules[i].startLifetime = lifeAcceleration * aMod;
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                mainModules[i].startSize = sizeVelocity * vMod;
                mainModules[i].startLifetime = lifeVelocity * vMod;
            }
        }
    }
}
