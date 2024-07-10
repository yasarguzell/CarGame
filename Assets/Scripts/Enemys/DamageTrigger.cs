using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] int damage = 15;
    private void OnTriggerEnter(Collider other)
    {
        //print("!!! Gerçek araç gelince incele");

        PlayerControllerTest controller = other.GetComponentInParent<PlayerControllerTest>();
        if (controller != null)
        {
            controller.TakeDamage(damage);

            ZombileShooterProjectile projectile = gameObject.GetComponent<ZombileShooterProjectile>();
            if (projectile != null)
            {//Shooter
                projectile.ReturnToPool();
            }
            else
            {//Melee
            }


        }

    }
}
