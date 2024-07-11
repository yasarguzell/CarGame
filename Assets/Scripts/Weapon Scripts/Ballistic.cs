using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballistic : Effect
{
    Projectile thisProjectile;

    private void Start()
    {
        thisProjectile = this.transform.GetComponent<Projectile>();
    }

    private void OnTriggerEnter(Collider other)
    {

        EnemyBase eBase;
        if (!other.transform.TryGetComponent<EnemyBase>(out eBase)) { return;}
        eBase.TakeDamage((int)(thisProjectile.damage));
        if (particleTransform)
        {
            Transform particleEffect = GameObject.Instantiate(particleTransform, thisProjectile.targetTransform.position + Vector3.Scale((this.transform.position - thisProjectile.targetTransform.position), this.transform.TransformDirection(new Vector3(0.25f, 1f, -0.1f))), this.transform.rotation);
        }

        Destroy(this.gameObject);
    }
}
