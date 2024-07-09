using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankDestroyer : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);    //yok et
        }
    }
}
