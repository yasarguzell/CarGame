using CarGame.Car;
using DG.Tweening;
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
    protected Transform nearestTarget;
    [SerializeField] protected Transform targetsParent;

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
        anim = GetComponent<Animator>();
    }
    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
    }
    private void OnEnable()
    {
        agent.enabled = true;
    }
    private void OnDisable()
    {
        agent.enabled = false;
    }
    public virtual void Update()
    {
        if (isDead)
            return;

        nearestTarget = FindNearestTarget();
        if (nearestTarget != null)
        {
            distance = Vector3.Distance(transform.position, nearestTarget.position);
        }
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
    Transform FindNearestTarget()
    {
        Transform closestTarget = null;
        float minDistance = Mathf.Infinity;
        if (targetsParent)
        {

        
        foreach (Transform target in targetsParent)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestTarget = target;
            }
        }
        }
        return closestTarget;
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
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        if (!isDead)
        {
            print("Die()");
            agent.isStopped = true;
            agent.enabled = false;
            anim.SetTrigger(ANIM_DIE_TRIGGER_NAME);
            isDead = true;
            this.gameObject.GetComponent<Collider>().isTrigger = true;

        }

    }
    public void DieAnimEvent()
    {
        this.transform.DOScale(Vector3.zero, 1).OnComplete(() =>
        {
            print("�l�m animasyonu bittikten 1 saniyes sonra setacitve false oluyor.");
            transform.parent.gameObject.SetActive(false);
           
        }
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //Die();
            //other.GetComponent<CarController>().TakeDamage(1);
        }
    }

}
