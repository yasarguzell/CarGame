using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ObjectSpawner objectS = FindObjectOfType<ObjectSpawner>();
            objectS.SpawnGround();
            gameObject.SetActive(false);
            Debug.Log($"Player spawned");

        }
    }

}
