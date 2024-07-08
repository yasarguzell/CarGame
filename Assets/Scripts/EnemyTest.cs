using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour, IHealth
{
    [Header("Animation Constants")]
    private const string ANIM_ATTACK_BOOLNAME = "isAttack";
    private const string ANIM_DIE_TRIGGERNAME = "isDead";

    [Header("Component References")]
    private Animator anim;
    private NavMeshAgent agent;

    [Header("Enemy Stats")]
    [SerializeField] private int health;

    [Header("Target Settings")]
    [SerializeField] private Transform target;

    [Header("Movement Settings")]
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float stopDistance = 1f;
    [SerializeField] private float chaseDistance = 10f; 

    [Header("Patrol Settings")]
    [SerializeField] private Transform patrolPointsParent; 
    private int patrolPointsIndex; 

    private bool isDead;
    private bool isPatrolling;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        if (target == null)
        {
            Debug.LogError("Target object is not assigned!");
        }
    }

    void Update()
    {
        if (isDead)
            return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > chaseDistance)
        {
            Patrol();
        }
        else if (distance > stopDistance)
        {
            Chase();
        }
        else //if (distance<stopDistance)
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }
    void Patrol()
    {

        Debug.Log("Patrolling");
        if ((!agent.pathPending && agent.remainingDistance < 0.5f) || isPatrolling == false)
        {
            anim.SetBool(ANIM_ATTACK_BOOLNAME, false);

            patrolPointsParent.transform.SetParent(this.transform.parent);

            patrolPointsIndex = (patrolPointsIndex + 1) % patrolPointsParent.childCount;
            Transform patrolTarget = patrolPointsParent.transform.GetChild(patrolPointsIndex);

            agent.SetDestination(patrolTarget.position);

            isPatrolling = true;
        }
    }

    void Chase()
    {
        Debug.Log("Chasing");

        anim.SetBool(ANIM_ATTACK_BOOLNAME, false);

        patrolPointsParent.transform.SetParent(this.transform);

        agent.SetDestination(target.position);

        isPatrolling = false;           
    }

    void Attack()
    {
        Debug.Log("Attacking");
        anim.SetBool(ANIM_ATTACK_BOOLNAME, true); //damage transactions in animation event 
    }

    // Implement interface
    public float GetCurrentHealth()
    {
        return health;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        CheckHealth();
    }

    public void CheckHealth()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        agent.isStopped = true;
        agent.enabled = false;

        anim.SetTrigger(ANIM_DIE_TRIGGERNAME);

        isDead = true;
    }
}
