using CarGame.Car;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] int damage = 15;
    [SerializeField] AudioSource meleeSource;
    private ZombileShooterProjectile storedProjectile;
    private void OnTriggerEnter(Collider other)
    {
        CarController controller = other.GetComponentInParent<CarController>();
        if (controller != null)
        {
            //controller.TakeDamage(damage);
            ZombileShooterProjectile projectile = gameObject.GetComponent<ZombileShooterProjectile>();
            if (projectile != null)
            {//Shooter
                storedProjectile = projectile;
                Invoke(nameof(CallReturnToPool), 5);
            }
            else
            {//Melee
                meleeSource.Play();
            }


        }

    }

    void CallReturnToPool()
    {
        returnToPool(storedProjectile);
    }

    void returnToPool(ZombileShooterProjectile projectile)
    {
      
        projectile.ReturnToPool();
      
    }
}
