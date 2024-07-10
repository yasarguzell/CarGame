using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unstable : Projectile
{
    [Header("Technical Variables")]
    [SerializeField] Transform jitterObject;
    [SerializeField] float jitterDeviation;
    [SerializeField] float jitterFrequency;
    [SerializeField] float jitterSpeed;

    private Vector3 currentJitterTarget;//Local
    private float targetStartDistance;

    private void Start()
    {
        targetStartDistance = Vector3.Distance(this.transform.position, targetTransform.position);

        StartCoroutine(ChangeJitterTarget());
        StartCoroutine(MoveTowardsTarget());
    }
    private void FixedUpdate()
    {
        if (pointTowardsVelocity) PointTowardsVelocity();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this);
            thisRigidbody.velocity = Vector3.zero;
        }
    }

    IEnumerator ChangeJitterTarget()
    {
        while (true)
        {
            if(jitterFrequency > 0.01f)
            currentJitterTarget = new Vector3(Random.Range(-1f, 1f) * jitterDeviation, Random.Range(-1f, 1f) * jitterDeviation, 0f);
            yield return new WaitForSeconds(1f / jitterFrequency);
        }
    }
    IEnumerator MoveTowardsTarget()
    {
        while (true)
        {
            Vector3 dir = (currentJitterTarget - jitterObject.localPosition).normalized;
            jitterObject.localPosition += jitterSpeed * dir * Time.deltaTime;

            float distancePercentage = 1f - (targetStartDistance - Vector3.Distance(this.transform.position, targetTransform.position)) / targetStartDistance;
            jitterObject.localPosition -= jitterObject.localPosition * Mathf.Sqrt(Mathf.Abs((distancePercentage - 0.5f) * 2f));
            yield return new WaitForEndOfFrame();
        }
    }
}
