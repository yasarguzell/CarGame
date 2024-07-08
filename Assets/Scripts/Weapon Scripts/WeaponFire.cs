using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : Weapon
{
    [Header("Weapon Properties")]
    [SerializeField] Transform projectile;
    [SerializeField] Transform exitPosition;
    [SerializeField][Tooltip("Per Second")] float fireRate = 1f;
    [SerializeField] float exitVelocity = 100f;
    [SerializeField][Range(0.0f, 1.0f)] float exitVelocityRandomness = 0f;
    [SerializeField][Range(0.0f, 1.0f)] float directionRandomness = 0f;

    private bool isFireOn = true;
    private float lastFireTime = 0f;

    void Start()
    {
        StartCoroutine(FireCoroutine());
    }

    void Update()
    {
        
    }

    void Fire()
    {
        lastFireTime = Time.time;

        Transform projectileTransform = GameObject.Instantiate(projectile, exitPosition.position, exitPosition.rotation);
        Rigidbody projectileRigidbody = projectileTransform.GetComponent<Rigidbody>();

        projectileRigidbody.velocity = exitPosition.forward * exitVelocity + exitPosition.forward * exitVelocity * Random.Range(-exitVelocityRandomness, exitVelocityRandomness);
    }

    IEnumerator FireCoroutine()
    {
        while (isFireOn)
        {
            bool isOnCooldown = (Time.time - lastFireTime) < (1 / fireRate);

            if (!isOnCooldown)
            {
                Fire();
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
