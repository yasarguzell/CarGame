using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ObjectSpawner.instance.SpawnGround();
            Destroy(other.gameObject);
            Debug.Log($"Player spawned");

        }
    }

}
