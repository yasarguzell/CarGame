using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : Weapon
{
    [Header("Weapon Properties")]
    [SerializeField] Transform projectile;
    [SerializeField] Transform exitPosition;
    [SerializeField][Tooltip("Per Second")] float fireRate = 1f;
    [SerializeField] [Tooltip("Per Firing")] float burstCount = 1f;
    [SerializeField] float exitVelocity = 100f;
    [SerializeField][Range(0.0f, 1.0f)] float exitVelocityRandomness = 0f;
    [SerializeField][Range(0.0f, 1.0f)] float directionRandomness = 0f;

    private WeaponTargetLock weaponTargetLock;
    private bool isFireOn = true;
    private float lastFireTime = 0f;

    void Start()
    {
        weaponTargetLock = this.transform.GetComponent<WeaponTargetLock>();

        StartCoroutine(FireCoroutine());
    }

    void Fire()
    {
        lastFireTime = Time.time;

        Burst();
    }

    void Burst()
    {
        for (int i = 0; i < burstCount; i++)
        {
            Transform projectileTransform = GameObject.Instantiate(projectile, exitPosition.position, exitPosition.rotation);
            Rigidbody projectileRigidbody = projectileTransform.GetComponent<Rigidbody>();
            Projectile projectileP = projectileTransform.GetComponent<Projectile>();
            projectileP.targetTransform = weaponTargetLock.targetTransform;
            projectileP.spawnPointTransform = exitPosition;
            projectileP.thisRigidbody = projectileRigidbody;

            Vector3 dir = exitPosition.forward + new Vector3(Random.Range(-directionRandomness, directionRandomness), Random.Range(-directionRandomness, directionRandomness), Random.Range(-directionRandomness, directionRandomness));
            projectileRigidbody.velocity = dir * exitVelocity + dir * exitVelocity * Random.Range(-exitVelocityRandomness, exitVelocityRandomness);
        }
    }

    IEnumerator FireCoroutine()
    {
        while (isFireOn)
        {
            bool isOnCooldown = (Time.time - lastFireTime) < (1 / fireRate);

            if (!isOnCooldown && weaponTargetLock.targetTransform)
            {
                Fire();
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
