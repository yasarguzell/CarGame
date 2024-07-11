using CarGame.Car;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] int damage = 15;
    [SerializeField] AudioSource meleeSource;
    private void OnTriggerEnter(Collider other)
    {
        //print("!!! Gerçek araç gelince incele");
        CarController controller = other.GetComponentInParent<CarController>();
        if (controller != null)
        {
            controller.TakeDamage(damage);

            ZombileShooterProjectile projectile = gameObject.GetComponent<ZombileShooterProjectile>();
            if (projectile != null)
            {//Shooter
               StartCoroutine( returnToPool(projectile));
            }
            else
            {//Melee
                meleeSource.Play();
            }


        }

    }
    IEnumerator returnToPool(ZombileShooterProjectile projectile)
    {
        yield return new WaitForSeconds(2);
        this.transform.DOScale(new Vector3(0, 0, 0), 1).OnComplete(() =>
        {
            projectile.ReturnToPool();
        });
       
    }
}
