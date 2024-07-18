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
    [SerializeField] bool isDirectFire = false;
    [Space()]
    [Header("Weapon Sound")]
    [SerializeField] AudioSource audioSource;
    [Space()]
    [Header("Weapon Effect")]
    [SerializeField] Transform fireEffect;

    private WeaponTargetLock weaponTargetLock;
    private bool isFireOn = true;
    private float lastFireTime = 0f;
    private float startVolume;


    void Start()
    {
        weaponTargetLock = this.transform.GetComponent<WeaponTargetLock>();
        startVolume = audioSource.volume;
        audioSource.volume = 0f;
    }

    private void Update()
    {
        isFireOn = weaponTargetLock.targetTransform != null;

        CheckFire();
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

            Vector3 dir = Vector3.zero;
            if (isDirectFire)
            {
                dir = (((weaponTargetLock.targetTransform.position + weaponTargetLock.trackOffset) - exitPosition.position).normalized + new Vector3(Random.Range(-directionRandomness, directionRandomness), Random.Range(-directionRandomness, directionRandomness), Random.Range(-directionRandomness, directionRandomness))).normalized;
            }
            else
            {
                dir = (exitPosition.transform.forward + new Vector3(Random.Range(-directionRandomness, directionRandomness), Random.Range(-directionRandomness, directionRandomness), Random.Range(-directionRandomness, directionRandomness))).normalized;
            }
            projectileRigidbody.velocity = dir * exitVelocity + dir * exitVelocity * Random.Range(-exitVelocityRandomness, exitVelocityRandomness);
        }
    }

    void CheckFire()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        if (isFireOn)
        {
            bool isOnCooldown = (Time.time - lastFireTime) < (1 / fireRate);

            if (!isOnCooldown)
            {
                Fire();
            }

            audioSource.volume = Mathf.Lerp(audioSource.volume, startVolume, 0.3f);

            if (fireEffect) fireEffect.gameObject.SetActive(true);
        }
        else
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, 0.3f);
            if (fireEffect) fireEffect.gameObject.SetActive(false);
        }
    }
}
