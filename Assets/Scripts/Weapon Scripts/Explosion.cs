using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Effect
{
    Projectile thisProjectile;

    [SerializeField] float radius;

    private void Start()
    {
        thisProjectile = this.transform.GetComponent<Projectile>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, radius, ~0, QueryTriggerInteraction.UseGlobal);
        for (int i = 0; i < colliders.Length; i++)
        {
            EnemyBase eBase;

            if (colliders[i].transform.TryGetComponent<EnemyBase>(out eBase))
            {
                eBase.TakeDamage((int)(this.transform.GetComponent<Projectile>().damage / ((colliders[i].transform.root.position - this.transform.position).magnitude / radius)));
            }
        }

        GameObject.Instantiate(particleTransform, this.transform.position, this.transform.rotation);
        Destroy(this.transform.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, radius, ~0, QueryTriggerInteraction.UseGlobal);
        for (int i = 0; i < colliders.Length; i++)
        {
            EnemyBase eBase;

            if (colliders[i].transform.TryGetComponent<EnemyBase>(out eBase))
            {
                eBase.TakeDamage((int)(this.transform.GetComponent<Projectile>().damage / ((colliders[i].transform.root.position - this.transform.position).magnitude / radius)));
            }
        }

        GameObject.Instantiate(particleTransform, this.transform.position, this.transform.rotation);
        Destroy(this.transform.gameObject);
    }
}
