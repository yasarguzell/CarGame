using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour, IHealth, IEnemy
{
    [Header("Animation Constants")]
    protected const string ANIM_ATTACK_BOOL_NAME = "isAttack";
    protected const string ANIM_DIE_TRIGGER_NAME = "isDead";

    [Header("Component References")]
    protected Animator anim;
    protected NavMeshAgent agent;

    [Header("Enemy Stats")]
    [SerializeField] protected int health;

    [Header("Target Settings")]
    [SerializeField] protected Transform target;

    [Header("Movement Settings")]
    [SerializeField] protected float rotationSpeed = 360f;
    [SerializeField] protected float distance = 360f;
    [SerializeField] protected float attackDistance = 1f;
    [SerializeField] protected float chaseDistance = 10f;

    [Header("Patrol Settings")]
    [SerializeField] protected Transform patrolPointsParent;
    protected int patrolPointsIndex;

    protected bool isDead;
    protected bool isPatrolling;
    protected bool isAttacking;
    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        if (target == null)
        {
            Debug.LogError("Target object is not assigned!");
        }
    }
    public virtual void Update()
    {
        if (isDead)
            return;

         distance = Vector3.Distance(transform.position, target.position);

        if (distance > chaseDistance)
        {
            Patrol();
        }
        else if (distance > attackDistance)
        {
            Chase();
        }
        else //if (distance<stopDistance)
        {
            Attack();
        }

        

    }
    public abstract void Chase();

    public abstract void Patrol();

    public abstract void Attack();

    public virtual float GetCurrentHealth()
    {
        return health;
    }
    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        CheckHealth();
    }
    public virtual void CheckHealth()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        agent.isStopped = true;
        agent.enabled = false;
        anim.SetTrigger(ANIM_DIE_TRIGGER_NAME);
        isDead = true;
    }

}
