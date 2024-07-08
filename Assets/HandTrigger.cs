using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("!!! Gerçek araç gelince incele");
        PlayerControllerTest controller = other.GetComponentInParent<PlayerControllerTest>();
        if (controller != null)
        {
            controller.TakeDamage(15);
        }
    }
}
