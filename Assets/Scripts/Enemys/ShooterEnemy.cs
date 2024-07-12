using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : EnemyBase
{
    [SerializeField] private Transform _projectilePrefab;
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private ObjectPool _projectilePool;
    [SerializeField] private AudioSource _source;

    public override void Start()
    {
        base.Start();


    }
    public override void Update()
    {

        base.Update();
        if (targetsParent == null)
        {
            targetsParent = GameObject.FindGameObjectWithTag("DamagePShooting").transform;
        }

    }
    public override void Chase()
    {
        //Debug.Log("Chasing");

        anim.SetBool(ANIM_ATTACK_BOOL_NAME, false);

        patrolPointsParent.transform.SetParent(this.transform);


        Vector3 offset = nearestTarget.position - transform.forward * (attackDistance - .5f);
        agent.SetDestination(offset);

        isPatrolling = false;
    }
    public override void Patrol()
    {
        //Debug.Log("Patrolling");
        if ((!agent.pathPending && agent.remainingDistance < 0.5f) || isPatrolling == false)
        {
            anim.SetBool(ANIM_ATTACK_BOOL_NAME, false);

            patrolPointsParent.transform.SetParent(this.transform.parent);

            patrolPointsIndex = (patrolPointsIndex + 1) % patrolPointsParent.childCount;
            Transform patrolTarget = patrolPointsParent.transform.GetChild(patrolPointsIndex);

            agent.SetDestination(patrolTarget.position);

            isPatrolling = true;
        }

    }
    public override void Attack()
    {
        //Debug.Log("Attacking");
        transform.LookAt(nearestTarget.transform.position);
        anim.SetBool(ANIM_ATTACK_BOOL_NAME, true); //Damage transactions in animation event 
    }

    public void Shoot()//Animation Event
    {

        if (_shootingPoint != null)
        {
            // Get a projectile from the pool
            Transform projectileTransform = _projectilePool.Get();
            projectileTransform.position = _shootingPoint.position;
            projectileTransform.rotation = _shootingPoint.rotation;
            projectileTransform.gameObject.SetActive(true);

            // Initialize the projectile with the pool reference
            ZombileShooterProjectile projectile = projectileTransform.GetComponent<ZombileShooterProjectile>();
            projectile.Initialize(_projectilePool);

            // Calculate the direction to the target
            Vector3 directionToTarget = (nearestTarget.position - _shootingPoint.position).normalized;

            // Launch the projectile towards the target
            projectile.Throw(directionToTarget);

            _source.Play();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerControllerTest controller = other.GetComponentInParent<PlayerControllerTest>();
        if (controller != null)
        {
            Die();
        }
    }


}
